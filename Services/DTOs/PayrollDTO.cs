using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.DTOs
{
    public class PayrollDTO
    {
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public double SalarioLiquido { get; set; }
        public double SalarioBruto { get; set; }
        public double ImpostoIrrf { get; set; }
        public double ImpostoInss { get; set; }
        public double ImpostoFgts { get; set; }
        public long EmployeeId { get; set; }
    }
}