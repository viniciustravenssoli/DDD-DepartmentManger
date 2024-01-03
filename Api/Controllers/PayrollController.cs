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
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost]
        [Route("/api/v1/payroll/create")]

        public async Task<IActionResult> Create([FromBody] PayrollDTO payrollDTO)
        {
            try
            {
                var employeeCreated = await _payrollService.Create(payrollDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Folha de Pagamento criado com sucesso",
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

        [HttpGet]
        [Route("/api/v1/payroll/list/{userId}")]

        public async Task<IActionResult> GetEmployeePayrolls([FromRoute] int userId)
        {
            try
            {
                var payrollList = await _payrollService.GetAllEmployeePayroll(userId);

                return Ok(new ResultViewModel
                {
                    Message = "Folhas retornadas com sucesso",
                    Success = true,
                    Data = payrollList
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