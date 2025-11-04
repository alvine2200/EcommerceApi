using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Context;
using EcommerceApi.Dtos.Response;
using EcommerceApi.Helpers;
using EcommerceApi.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using EcommerceApi.Dtos;
using EcommerceApi.Models;
using EcommerceApi.Mapper;
using EcommerceApi.Exceptions;

namespace EcommerceApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email!))
                throw new Exception("Email already registered");

            var roles = await _roleRepository.GetRolesByNamesAsync(dto.Roles);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName!,
                Email = dto.Email!,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Roles = roles
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var (token, expires) = _jwtHelper.GenerateJwtToken(user);
            var (refreshToken, refreshExpires) = _jwtHelper.GenerateRefreshToken();

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAt = expires,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshExpires,
                user = UserResponseMapper.ToUserResponseDto(user)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.password, user.Password))
            {
                throw new NotFoundException("Invalid email or password");
            }
            var (token, expires) = _jwtHelper.GenerateJwtToken(user);
            var (refreshToken, refreshExpires) = _jwtHelper.GenerateRefreshToken();

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAt = expires,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshExpires,
                user = UserResponseMapper.ToUserResponseDto(user)
            };
        }

        public async Task<UserResponseDto?> GetCurrentUserAsync(string userId)
        {
            if (!Guid.TryParse(userId, out var guid))
                return null;

            var user = await _userRepository.GetByIdAsync(guid);
            return user != null ? UserResponseMapper.ToUserResponseDto(user) : null;
        }
        
        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var principal = _jwtHelper.GetPrincipalFromExpiredToken(refreshToken);
            var userId = principal!.Claims.First(c => c.Type == "id").Value;

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (user == null)
                throw new Exception("User not found");

            var (newToken, expires) = _jwtHelper.GenerateJwtToken(user);
            var (newRefreshToken, refreshExpires) = _jwtHelper.GenerateRefreshToken();

            return new AuthResponseDto
            {
                Token = newToken,
                ExpiresAt = expires,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiresAt = refreshExpires,
                user = UserResponseMapper.ToUserResponseDto(user)
            };
        }
    }
}