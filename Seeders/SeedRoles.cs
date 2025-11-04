using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;
using EcommerceApi.Enum;
using EcommerceApi.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Seeders
{
    public class SeedRoles
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.Migrate();

            if (!context.Permissions.Any())
            {
                var permissions = System.Enum.GetValues(typeof(PermissionEnum))
                    .Cast<PermissionEnum>()
                    .Select(p => new Permission
                    {
                        Id = Guid.NewGuid(),
                        Name = p,
                        Description = p.ToString()
                    })
                    .ToList();

                context.Permissions.AddRange(permissions);
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                var roles = System.Enum.GetValues(typeof(RoleEnum))
                    .Cast<RoleEnum>()
                    .Select(r => new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = r
                    })
                    .ToList();

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            var adminRole = context.Roles.Include(r => r.Permissions)
                                        .FirstOrDefault(r => r.Name == RoleEnum.ADMIN);

            if (adminRole != null && !adminRole.Permissions.Any())
            {
                var allPermissions = context.Permissions.ToList();
                adminRole.Permissions = allPermissions;
                context.SaveChanges();
            }
        }
    }
}