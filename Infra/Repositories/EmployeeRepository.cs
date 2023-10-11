using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Exceptions;
using Domain.Entities;
using Infra.Context;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee> GetByCpf(string cpf)
        {
            var employees = await _context.Employees.FirstOrDefaultAsync(x => x.Cpf == cpf);

            return employees;
        }

        public async Task<List<Employee>> GetByMouthOfJoining(DateTime dataEntrada)
        {
            var employee = await _context.Employees.Where(x => x.DataDeEntrada.Month == dataEntrada.Month).ToListAsync();

            return employee;
        }

        public async Task<List<Employee>> ListEmployeesSalaryByDepartmentId(int departmentId)
        {
            var employees = await _context.Employees.Where(x => x.DepartmentId == departmentId).OrderByDescending(x => x.SalarioAnual).ToListAsync();

            return employees;
        }

        public async Task<List<Employee>> GetEmployeesWithAboveAverageSalary()
        {
            var avrgSalary = await _context.Employees.AverageAsync(x => x.SalarioAnual);

            var employees = await _context.Employees.Where(x => x.SalarioAnual <= avrgSalary).ToListAsync();

            return employees;
        }

        public async Task<long> GetNumbersOfEmployeesByDepartament(long departmentId)
        {
            var employees = await _context.Employees.CountAsync(x => x.DepartmentId == departmentId);

            return employees;
        }

    }
}