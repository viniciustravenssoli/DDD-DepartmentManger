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
                                    .AsNoTracking()
                                    .ToListAsync();
                                    
            return user.FirstOrDefault();
        }
    }
}