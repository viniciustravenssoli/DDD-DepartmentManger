using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.EntitiesConstants;
using FluentValidation;

namespace Domain.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade nao pode ser vazia")

                .NotNull()
                .WithMessage("A entidade nao pode ser nula.");

            RuleFor(x => x.Nome)
                .NotNull()
                .Length(EmployeeConstants.MinName, EmployeeConstants.MaxName)
                .WithMessage("O nome nao pode ser nulo menor que 3 ou maior que 80")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio");

            RuleFor(x => x.Cpf)
                .NotNull()
                .WithMessage("O Cpf nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio")

                .Length(11, 11)
                .WithMessage("O cpf deve ter 11 caracteres")

                .Matches("^[0-9]*$")
                .WithMessage("O CPF deve conter apenas números");
        }
    }
}