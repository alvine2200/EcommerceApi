using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dtos.Response
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public UserResponseDto? user { get; set; }
    }

}