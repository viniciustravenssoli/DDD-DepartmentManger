using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetByCpf(string cpf);
        Task<Employee> GetById(long id);
        Task<List<Employee>> GetByMouthOfJoining(DateTime dataEntrada);
        Task<long> GetNumbersOfEmployeesByDepartament(long departmentId);
    }
}