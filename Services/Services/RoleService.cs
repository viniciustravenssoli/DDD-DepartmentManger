using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Interfaces;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRolesRepository _roleRepository;

        public RoleService(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> Create(string roleName)
        {
            var roleExists = await _roleRepository.FindByName(roleName);

            if (roleExists != null)
                throw new Exception("A Role ja existe.");

            var role = new Role(roleName);

            await _roleRepository.Create(role);

            return role;
        }

        
    }
}