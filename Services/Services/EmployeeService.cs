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
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeRepository = employeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Create(EmployeeDto employeeDto)
        {
            var department = await _departmentRepository.GetDepartmentIncludeEmployee(employeeDto.DepartmentId);

            if (department == null)
            {
                throw new DomainExceptions($"O departamento com Id informado não existe, por favor insira um valor valido");
            }

            if (department.AtingiuLimiteFuncionarios())
            {
                throw new DomainExceptions($"O departamento {department.DepartmentName} atingiu seu limite máximo de {department.EmployeeLimit} funcionários");
            }
            // var employeeQntd = await _employeRepository.GetNumbersOfEmployeesByDepartament(employeeDto.DepartmentId);
            // ValidateDepartment(department);
            // ValidateEmployeeLimit(employeeQntd, department);
            var emplyoee = new Employee(employeeDto.Nome, employeeDto.Cpf, employeeDto.Email, employeeDto.DataDeEntrada, employeeDto.DepartmentId)
            {
                Department = department
            };

            var employeeCreated = await _employeRepository.Create(emplyoee);

            return _mapper.Map<EmployeeDto>(employeeCreated);
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

        public async Task<EmployeeDto> Update(EmployeeDto employeeDto)
        {
            var employeeExists = await _employeRepository.Get(employeeDto.Id);

            if (employeeExists == null)
                throw new DomainExceptions("Nao existe nenhum employee com o Id informado");
            
            var employee = _mapper.Map<Employee>(employeeDto);

            var employeeUpdated = await _employeRepository.Update(employee);

            return _mapper.Map<EmployeeDto>(employeeUpdated);
        }

        public async Task Remove(long id)
        {
            await _employeRepository.Remove(id);
        }

        public async Task<EmployeeDto> Get(long id)
        {
            var employee = await _employeRepository.Get(id);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<List<EmployeeDto>> Get()
        {
            var employees = await _employeRepository.Get();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }
    }
}