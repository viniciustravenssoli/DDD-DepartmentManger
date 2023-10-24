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
    public class PayrollRepository : BaseRepository<Payroll>, IPayrollRepository
    {
        private readonly AppDbContext _context;
        public PayrollRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}