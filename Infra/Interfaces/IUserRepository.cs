using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<bool> AddRoleToUser(long userId, string roleName);
        Task<User> GetByEmailUserWithRoles(string email);
    }
}