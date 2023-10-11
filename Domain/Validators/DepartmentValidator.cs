using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
         public DepartmentValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade nao pode ser vazia")

                .NotNull()
                .WithMessage("A entidade nao pode ser nula.");

            RuleFor(x => x.DepartmentName)
                .NotNull()
                .WithMessage("O nome nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio")

                .MinimumLength(2)
                .WithMessage("O nome deve ter 2 ou mais caracteres")

                .MaximumLength(80)
                .WithMessage("O Nome deve ter no maximo 80 caracteres");
        }
    }
}