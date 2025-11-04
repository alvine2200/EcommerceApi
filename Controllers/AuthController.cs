using System;
using System.Net;
using System.Threading.Tasks;
using EcommerceApi.Dtos;
using EcommerceApi.Dtos.Response;
using EcommerceApi.Helpers;
using EcommerceApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.BadRequest("Invalid request data"));

            try
            {
                var result = await _authService.RegisterAsync(dto);
                return StatusCode((int)HttpStatusCode.Created,
                    ApiResponse<AuthResponseDto>.Created(result, "User registered successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Login and get access/refresh tokens
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.BadRequest("Invalid login data"));

            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized(ApiResponse<object>.Unauthorized("Invalid credentials"));

            return Ok(ApiResponse<AuthResponseDto>.Success(result, "Login successful"));
        }

        /// <summary>
        /// Refresh the JWT using a valid refresh token
        /// </summary>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(ApiResponse<object>.BadRequest("Refresh token is required"));

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (result == null)
                return Unauthorized(ApiResponse<object>.Unauthorized("Invalid or expired refresh token"));

            return Ok(ApiResponse<AuthResponseDto>.Success(result, "Token refreshed successfully"));
        }

        /// <summary>
        /// Get the currently logged-in user's profile
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User?.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(ApiResponse<object>.Unauthorized("Invalid or expired token"));

            var user = await _authService.GetCurrentUserAsync(userId);

            if (user == null)
                return NotFound(ApiResponse<object>.NotFound("User not found"));

            return Ok(ApiResponse<UserResponseDto>.Success(user, "User details retrieved successfully"));
        }
    }
}
