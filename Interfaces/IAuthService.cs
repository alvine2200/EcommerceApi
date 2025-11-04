using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dtos;
using EcommerceApi.Dtos.Response;

namespace EcommerceApi.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<UserResponseDto?> GetCurrentUserAsync(string userId);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    }
}