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


        public async Task<long> GetNumbersOfEmployeesByDepartament(long departmentId)
        {
            var employees = await _context.Employees.CountAsync(x => x.DepartmentId == departmentId);

            return employees;
        }

        public async Task<Employee> GetById(long id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            return employee;
        }
    }
}