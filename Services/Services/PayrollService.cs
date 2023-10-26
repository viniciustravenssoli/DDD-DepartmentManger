using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Exceptions;
using Domain.Entities;
using Infra.Interfaces;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    
    public class PayrollService : IPayrollService
    {
        private readonly IMapper _mapper;
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollService(IMapper mapper, IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<PayrollDTO> Create(PayrollDTO payrollDTO)
        {
            var employee = await _employeeRepository.GetById(payrollDTO.EmployeeId);

            if (employee == null)
            {
                throw new DomainExceptions("Não existe employee com esse id");
            }

            Payroll folha = new(payrollDTO.Valor, payrollDTO.Quantidade)
            {
                Employee = employee
            };

            var payrollCreated = await _payrollRepository.Create(folha);

            return _mapper.Map<PayrollDTO>(payrollCreated);
        }

        public Task<PayrollDTO> Update(PayrollDTO payrollDTO)
        {
            throw new NotImplementedException();
        }
    }
}