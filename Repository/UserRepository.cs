using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Context;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

        public async Task<bool> EmailExistsAsync(string email) =>
            await _context.Users.AnyAsync(u => u.Email == email);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}