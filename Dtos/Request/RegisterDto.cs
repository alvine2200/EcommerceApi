using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Enum;

namespace EcommerceApi.Dtos
{
    public class RegisterDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<RoleEnum> Roles { get; set; } = new() { RoleEnum.CUSTOMER };
    }
}