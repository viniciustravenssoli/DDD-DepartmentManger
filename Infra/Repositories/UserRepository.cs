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

        public async Task AddRoleToUser(long userId, string roleName)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);

            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (role == null)
            {
                // Se a função não existir, crie uma nova
                role = new Role(roleName);
                _context.Roles.Add(role);
            }

            if (user != null)
            {
                user.AddRole(role);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }
    }
}