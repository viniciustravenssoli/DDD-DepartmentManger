using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> Create(string roleName);
    }
}