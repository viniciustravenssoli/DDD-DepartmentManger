using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Context;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                                    .Where
                                    (
                                        x => x.Email.Adress.ToLower() == email.ToLower()
                                    )
                                    .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<bool> AddRoleToUser(long userId, string roleName)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (user != null && role != null)
            {
                user.AddRole(role);
                await _context.SaveChangesAsync();
                return true; 
            }
            return false;
        }
    }
}