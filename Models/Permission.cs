using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public PermissionEnum Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}