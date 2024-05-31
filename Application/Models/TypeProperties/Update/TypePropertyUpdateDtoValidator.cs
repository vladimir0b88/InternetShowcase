using FluentValidation;

namespace Application.Models
{
    public class TypePropertyUpdateDtoValidator : AbstractValidator<TypePropertyUpdateDto>
    {
        public TypePropertyUpdateDtoValidator()
        {
            RuleFor(tp => tp.Name).NotEmpty();

            RuleFor(tp => tp.Name).NotEmpty().MaximumLength(64);
        }
    }
}
