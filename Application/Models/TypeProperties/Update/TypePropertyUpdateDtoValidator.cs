using FluentValidation;

namespace Application.Models.TypeProperties.Update
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
