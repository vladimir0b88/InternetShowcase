using Application.Common;
using FluentValidation;

namespace Application.Models
{
    public class TypePropertyAddDtoValidator : AbstractValidator<TypePropertyAddDto>
    {
        public TypePropertyAddDtoValidator(IProductTypeRepository productTypeRepository)
        {
            RuleFor(tp => tp.Name).NotEmpty().MaximumLength(64);

            RuleFor(tp => tp.TypeId).Custom(async (typeId, context) =>
            {
                if (typeId is not null)
                {
                    bool typeExist = await productTypeRepository.ExistByIdAsync((long)typeId);

                    if (!typeExist)
                        context.AddFailure($"Указанный тип продукта с id: {typeId} не существует");
                }
            });
        }
    }
}
