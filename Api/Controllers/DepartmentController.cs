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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        [Route("/api/v1/department/create")]
        public async Task<IActionResult> Create([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                var employeeCreated = await _departmentService.Create(departmentDto);

                return Ok(new ResultViewModel
                {
                    Message = "Departamento criado com sucesso",
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
        [Route("/api/v1/department/update")]
        public async Task<IActionResult> Update([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                var employeeUpdated = await _departmentService.Update(departmentDto);

                return Ok(new ResultViewModel
                {
                    Message = "Departamento atualizado com sucesso",
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
    }
}