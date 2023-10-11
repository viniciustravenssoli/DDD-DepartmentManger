using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Domain.Validators;


namespace Domain.Entities
{
    public class Employee : Base
    {
        public Employee(string nome, string cpf, string email, double salario, double salarioAnual, DateTime dataDeEntrada, long departmentId)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Salario = salario;
            SalarioAnual = salarioAnual;
            DataDeEntrada = dataDeEntrada;
            DepartmentId = departmentId;

            _errors = new List<string>();
            Validate();
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public double Salario { get; set; }
        public double SalarioAnual { get; set; }
        public DateTime DataDeEntrada { get; set; }

        
        public long DepartmentId { get; set; }
        public Department? Department { get; set; }

        public override bool Validate()
        {
            var validator = new EmployeeValidator();
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

            foreach(var error in _errors)
                builder.AppendLine(error);

            return builder.ToString();
        }
    }
}