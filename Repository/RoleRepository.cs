using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Context;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using EcommerceApi.Enum;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext _context;
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRolesByNamesAsync(List<RoleEnum> names)
        {
            return await _context.Roles.Where(r => names.Contains(r.Name!.Value)).ToListAsync();
        }

        public async Task<Role?> GetByNameAsync(RoleEnum name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<Role?> GetByIdAsync(Guid id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}