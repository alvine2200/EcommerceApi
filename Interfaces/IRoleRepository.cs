using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;
using EcommerceApi.Enum;

namespace EcommerceApi.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRolesByNamesAsync(List<RoleEnum> names);
        Task<Role?> GetByNameAsync(RoleEnum name);
    }
}