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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> AddRole([FromBody] AddRole addRole)
        {
            try
            {
                await _userService.AddRoleToUser(addRole.UserId, addRole.Role);

                return Ok(new ResultViewModel
                {
                    Message = "Role Adicionada com sucesso",
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