using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    [Table("Roles")]
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public RoleEnum? Name { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}