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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeRepository, IDepartmentRepository departmentRepository, IDepartmentService departmentService, IMapper mapper)
        {
            _employeRepository = employeRepository;
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Create(EmployeeDto employeeDto)
        {
            var department = await _departmentRepository.Get(employeeDto.DepartmentId);

            var employeeQntd = await _employeRepository.GetNumbersOfEmployeesByDepartament(employeeDto.DepartmentId);

            ValidateDepartment(department);

            ValidateEmployeeLimit(employeeQntd, department);

            var employeee = _mapper.Map<Employee>(employeeDto);

            var employeeCreated = await _employeRepository.Create(employeee);
            employeeCreated.Department = department;

            return _mapper.Map<EmployeeDto>(employeeCreated);
        }

        private void ValidateDepartment(Department department)
        {
            if (department == null)
            {
                throw new DomainExceptions("Nenhum departamento encontrado com o Id fornecido, por favor verifique");
            }
        }


        private void ValidateEmployeeLimit(long employeeCount, Department department)
        {
            if (employeeCount >= department.EmployeeLimit)
            {
                throw new DomainExceptions($"O departamento {department.DepartmentName} atingiu seu limite máximo de {department.EmployeeLimit} funcionários");
            }
        }


        private async Task<bool> IsDepartmentFull(long departmentId)
        {
            var department = await _departmentRepository.Get(departmentId);

            var employees = await _employeRepository.GetNumbersOfEmployeesByDepartament(departmentId);

            if (employees >= department.EmployeeLimit)
            {
                return true;
            }
            return false;
        }
    }
}