using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<UserCreateDTO> Create(UserCreateDTO userCreateDTO);
        Task<string> Login(UserLoginDTO userLoginDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task<UserDTO> GetDepartmentById(long userId);
        Task Remove(long id);
        Task<UserDTO> Get(long id);
        Task<List<UserDTO>> Get();
        Task<bool> AddRoleToUser(long userId, string roleName);
    }
}