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
    public class RoleRepository : BaseRepository<Role>, IRolesRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role> FindByName(string roleName)
        {
            var role = await _context.Roles
                                    .Where
                                    (
                                        x => x.RoleName.ToLower() == roleName.ToLower()
                                    )
                                    .ToListAsync();

            return role.FirstOrDefault();
        }
    }
}