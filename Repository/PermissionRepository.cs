using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Context;
using EcommerceApi.Exceptions;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        public readonly ApplicationContext _context;
        public readonly IRoleRepository _roleRepository;

        public readonly IUserRepository _userRepository;

        public PermissionRepository(ApplicationContext context, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }


        public async Task<bool> UserHasPermissionAsync(Guid userId, Guid permissionId)
        {
            User? user = _userRepository.GetByIdAsync(userId).Result;
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var userRoles = user.Roles;
            foreach (var role in userRoles)
            {
                var permissions = await GetPermissionsByRoleIdAsync(role.Id);
                if (permissions.Any(p => p.Id == permissionId))
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<List<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            Role? role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }
            var permissions = role.Permissions;
            return permissions.ToList();
        }

        public Task<Permission?> GetByNameAsync(string name)
        {
            return _context.Permissions.FirstOrDefaultAsync(p => p.Name.ToString() == name);
        }
    }

}