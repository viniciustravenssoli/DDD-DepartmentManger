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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if(userExists != null)
                throw new Exception("Já existe um usuário cadastrado com o email informado.");


            var user = new User(userDTO.Email, userDTO.Password);

            await _userRepository.Create(user);

            return userDTO;
        }

        public Task<UserDTO> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDTO>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetDepartmentById(long userId)
        {
            throw new NotImplementedException();
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Update(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}