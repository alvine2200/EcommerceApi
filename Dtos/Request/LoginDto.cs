using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dtos
{
    public class LoginDto
    {
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}