using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
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
                .WithMessage("O nome nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio")

                .MinimumLength(3)
                .WithMessage("O nome deve ter 3 ou mais caracteres")

                .MaximumLength(80)
                .WithMessage("O Nome deve ter no maximo 30 caracteres");

            RuleFor(x => x.Cpf)
                .NotNull()
                .WithMessage("O Cpf nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio")

                .MinimumLength(11)
                .WithMessage("O cpf deve ter 11 ou mais caracteres")

                .MaximumLength(11)
                .WithMessage("O cpf deve ter no maximo 11 caracteres");
        }
    }
}