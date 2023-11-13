using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Reponses;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost]
        [Route("/api/v1/user/create")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            try
            {
                var userCreated = await _userService.Create(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario criado com sucesso",
                    Success = true,
                    Data = userCreated
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPost]
        [Route("/api/v1/user/login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            try
            {
                var loggedUser = await _userService.Login(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario logado com sucesso",
                    Success = true,
                    Data = loggedUser
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPost]
        [Route("/api/v1/user/AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleToUserDTO addRoleToUserDTO)
        {
            try
            {
                await _userService.AddRoleToUser(addRoleToUserDTO.UserId, addRoleToUserDTO.Role);

                return Ok(new ResultViewModel
                {
                    Message = "Role vinculada ao usuario com sucesso",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPost]
        [Route("/api/v1/user/Create-Role")]
        public async Task<IActionResult> CreateRole([FromBody] AddRole addRole)
        {
            try
            {
                await _roleService.Create(addRole.RoleName);

                return Ok(new ResultViewModel
                {
                    Message = "Role Criada com sucesso",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
        

    }
}