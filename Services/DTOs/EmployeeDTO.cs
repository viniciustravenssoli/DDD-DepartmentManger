using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.DTOs
{
    public class EmployeeDto
    {

        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime DataDeEntrada { get; set; }
        public long DepartmentId { get; set; }

    }
}