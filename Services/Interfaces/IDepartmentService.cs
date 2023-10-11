using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> Create(DepartmentDto departmentDto);
        Task<DepartmentDto> GetDepartmentById(long departmentId);
    }
}