using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IPayrollRepository : IBaseRepository<Payroll>
    {
        Task<List<Payroll>> GetAllPayrollsFromUser(int id);
    }
}