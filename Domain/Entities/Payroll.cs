using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
    public class Payroll : Base
    {
        public Payroll(double valor, int quantidade)
        {
            Valor = valor;
            Quantidade = quantidade;
            CalcularSalarioBruto();
            CalcularFgts();
            CalcularIrrf();
            CalcularInss();
            CalcularSalarioLiquido();

            _errors = new List<string>();
            Validate();
        }

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
        public Employee? Employee { get; set; }

        private void CalcularSalarioBruto() =>
            SalarioBruto = Valor * Quantidade;

        private void CalcularSalarioLiquido() =>
            SalarioLiquido = SalarioBruto - ImpostoIrrf - ImpostoInss;

        private void CalcularFgts() =>
            ImpostoFgts = SalarioBruto * .08;

        private void CalcularInss()
        {
            if (SalarioBruto <= 1693.72)
                ImpostoInss = SalarioBruto * .08;
            else if (SalarioBruto <= 2822.9)
                ImpostoInss = SalarioBruto * .09;
            else if (SalarioBruto <= 5645.8)
                ImpostoInss = SalarioBruto * .11;
            else
                ImpostoInss = 621.03;
        }

        private void CalcularIrrf()
        {
            if (SalarioBruto <= 1903.98)
                ImpostoIrrf = 0;
            else if (SalarioBruto <= 2826.65)
                ImpostoIrrf = SalarioBruto * .075 - 142.8;
            else if (SalarioBruto <= 3751.05)
                ImpostoIrrf = SalarioBruto * .15 - 354.8;
            else if (SalarioBruto <= 4664.68)
                ImpostoIrrf = SalarioBruto * .225 - 636.13;
            else
                ImpostoIrrf = SalarioBruto * .275 - 869.39;
        }

         public override bool Validate()
        {
            var validator = new PayrollValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _errors.Add(error.ErrorMessage);

                throw new DomainExceptions("Alguns campos estão inválidos, por favor corrija-os!", _errors);
            }

            return true;
        }

        public string ErrorsToString()
        {
            var builder = new StringBuilder();

            foreach (var error in _errors)
                builder.AppendLine(error);

            return builder.ToString();
        }
    }
}