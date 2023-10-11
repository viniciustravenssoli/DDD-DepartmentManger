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
    }
}