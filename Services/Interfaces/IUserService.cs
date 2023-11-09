using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task<UserDTO> GetDepartmentById(long userId);
        Task Remove(long id);
        Task<UserDTO> Get(long id);
        Task<List<UserDTO>> Get();
    }
}