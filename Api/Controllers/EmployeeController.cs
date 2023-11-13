using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Reponses;
using AutoMapper;
using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/employee/create")]

        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employeeCreated = await _employeeService.Create(employeeDto);

                return Ok(new ResultViewModel
                {
                    Message = "Funcionario criado com sucesso",
                    Success = true,
                    Data = employeeCreated
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

        [HttpPut]
        [Route("/api/v1/employee/update")]
        public async Task<IActionResult> Update([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employeeUpdated = await _employeeService.Update(employeeDto);

                return Ok(new ResultViewModel
                {
                    Message = "Funcionario atualizado com sucesso",
                    Success = true,
                    Data = employeeUpdated
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

        [Authorize]
        [HttpGet]
        [Route("/api/v1/employee/get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _employeeService.Get();

                return Ok(new ResultViewModel
                {
                    Message = "Funcionarios retornados com sucesso",
                    Success = true,
                    Data = employees
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

        [HttpGet]
        [Route("/api/v1/employee/get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var employee = await _employeeService.Get(id);

                return Ok(new ResultViewModel
                {
                    Message = "Funcionario retornado com sucesso",
                    Success = true,
                    Data = employee
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