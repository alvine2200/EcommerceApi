using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = null!;
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}