using Application.Common;
using FluentValidation;

namespace Application.Models
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator(IProductTypeRepository productTypeRepository)
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);

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
