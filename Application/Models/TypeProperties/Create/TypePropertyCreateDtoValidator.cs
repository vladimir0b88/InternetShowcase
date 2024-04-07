using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Models.TypeProperties.Create
{
    public class TypePropertyCreateDtoValidator : AbstractValidator<TypePropertyCreateDto>
    {
        public TypePropertyCreateDtoValidator(IProductTypeRepository productTypeRepository)
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            RuleFor(p => p.TypeId).Custom(async (typeId, context) =>
            {
                if (typeId is not null)
                {
                    bool typeExist = await productTypeRepository.ExistById((long)typeId);

                    if (!typeExist)
                        context.AddFailure($"Указанный тип продукта с id: {typeId} не существует");
                }
            });
        }
    }
}
