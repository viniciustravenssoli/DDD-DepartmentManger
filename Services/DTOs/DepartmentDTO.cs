using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.DTOs
{
    public class DepartmentDto
    {
        public DepartmentDto(long id, string? departmentName, int employeeLimit)
        {
            Id = id;
            DepartmentName = departmentName;
            EmployeeLimit = employeeLimit;
        }

        public long Id { get; set; }
        public string? DepartmentName { get; set; }
        public int EmployeeLimit { get; set; }

    }
}