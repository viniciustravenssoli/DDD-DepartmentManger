using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class PayrollValidator : AbstractValidator<Payroll>
    {
        public PayrollValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade nao pode ser vazia")

                .NotNull()
                .WithMessage("A entidade nao pode ser nula.");

            RuleFor(x => x.Quantidade)
                .NotEmpty()
                .WithMessage("Por favor informe a quantidade de horas que o funcionario trabalhou no mes")

                .NotNull()
                .WithMessage("Por favor informe a quantidade de horas que o funcionario trabalhou no mes");

            RuleFor(x => x.Valor)
                .NotEmpty()
                .WithMessage("Por favor informe o valor da hora do funcionario")

                .NotNull()
                .WithMessage("Por favor informe o valor da hora do funcionario");
        }
    }
}