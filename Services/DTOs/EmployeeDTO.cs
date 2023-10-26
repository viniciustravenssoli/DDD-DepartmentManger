using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.DTOs
{
    public class EmployeeDto
    {

        public long Id { get; set; }
        public string Nome { get; set; }

        //[Required(ErrorMessage = "O campo CPF é obrigatório.")]
        //[StringLength(11, ErrorMessage = "O campo deve ter exatamente 11 caracteres.")]
        public string Cpf { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DataDeEntrada { get; set; }
        public long DepartmentId { get; set; }

    }
}