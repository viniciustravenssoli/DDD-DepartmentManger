using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Domain.Entities;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserCreateDTO> Create(UserCreateDTO userCreateDTO)
        {
            var userExists = await _userRepository.GetByEmail(userCreateDTO.Email);

            if (userExists != null)
                throw new Exception("Já existe um usuário cadastrado com o email informado.");

            userCreateDTO.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password);

            var user = new User(userCreateDTO.Email, userCreateDTO.Password);

            await _userRepository.Create(user);

            return userCreateDTO;
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

        public async Task<string> Login(UserLoginDTO userLoginDTO)
        {
            var userExists = await _userRepository.GetByEmailUserWithRoles(userLoginDTO.Email);

            if (userExists is null)
                throw new DomainExceptions("Usuario ou senha incorreto, por favor verifique email e senha");

            var correctPass = BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, userExists.PasswordHash.Pass);

            if (!correctPass)
            {
                throw new DomainExceptions("Usuario ou Senha incorreto, por favor verifique email e senha");
            }

            var token = GenerateToken(userExists);

            return token;

        }

        public async Task<bool> AddRoleToUser(long userId, string roleName) // Suponhamos que você tenha o ID do usuário e o nome da função
        {
            var result = await _userRepository.AddRoleToUser(userId, roleName);

            if (result is false)
            {
                throw new DomainExceptions("Usuario ou role nao encontrado, por favor verifique");
            }
            return true;
        }


        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Update(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = ManageClains(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(3),
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        private ClaimsIdentity ManageClains(User user)
        {
            var cli = new ClaimsIdentity();
            cli.AddClaim(new Claim("Id", user.Id.ToString()));
            cli.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            foreach (var role in user.Roles)
                cli.AddClaim(new Claim(ClaimTypes.Role, role));

            return cli;
        }
    }
}