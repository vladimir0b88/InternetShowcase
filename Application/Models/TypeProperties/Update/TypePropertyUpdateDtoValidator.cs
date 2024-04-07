using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.TypeProperties.Update
{
    public class TypePropertyUpdateDtoValidator : AbstractValidator<TypePropertyUpdateDto>
    {
        public TypePropertyUpdateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();

            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
