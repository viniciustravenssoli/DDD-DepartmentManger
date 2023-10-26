using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> Create(EmployeeDto employeeDto);
        Task<EmployeeDto> Update(EmployeeDto employeeDto);
        Task Remove(long id);
        Task<EmployeeDto> Get(long id);
        Task<List<EmployeeDto>> Get();
    }
}