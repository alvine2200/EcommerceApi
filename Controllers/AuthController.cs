using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Helpers;
using EcommerceApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EcommerceApi.Dtos.Response;
using EcommerceApi.Dtos;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            var response = ApiResponse<AuthResponseDto>.Created(result, "User registered successfully");
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized(ApiResponse<AuthResponseDto>.Fail(HttpStatusCode.Unauthorized, "Invalid credentials"));
            }

            var response = ApiResponse<AuthResponseDto>.Success(result, "Login successful");
            return StatusCode((int)response.StatusCode, response);
        }
    }

}