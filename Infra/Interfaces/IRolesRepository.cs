using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Role>
    {
        Task<Role> FindByName(string roleName);
    }
}