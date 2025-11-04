using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;

namespace EcommerceApi.Interfaces
{
    public interface IPermissionRepository
    {
        public Task<List<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);
        public Task<Permission?> GetByNameAsync(string name);
    }
}