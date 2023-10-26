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
        Task<DepartmentDto> Update(DepartmentDto departmentDto);
        Task<DepartmentDto> GetDepartmentById(long departmentId);
        Task Remove(long id);
        Task<DepartmentDto> Get(long id);
        Task<List<DepartmentDto>> Get();
        
    }
}