using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Context;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Department> GetByName(string name)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentName.ToLower() == name.ToLower());

            return department;
        }

        public async Task<Department> GetDepartmentIncludeEmployee(long id)
        {
            var department = await _context.Departments.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);

            return department;
        }
    }
}