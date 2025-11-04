using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Context;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using EcommerceApi.Models;
using EcommerceApi.Models;

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
        public async Task<bool> UserHasPermissionAsync(Guid userId, string permissionName)
        {

            User? user = _userRepository.GetByIdAsync(userId).Result;
            if (user == null)
            {
                throw new Exception("User not found");
            }
            // Implement logic to check if the user has the specified permission
            throw new NotImplementedException();
        }
    }
}