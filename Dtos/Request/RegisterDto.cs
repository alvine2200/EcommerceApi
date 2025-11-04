using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Enum;

namespace EcommerceApi.Dtos
{
    public class RegisterDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<RoleEnum> Roles { get; set; } = new() { RoleEnum.CUSTOMER };
    }
}