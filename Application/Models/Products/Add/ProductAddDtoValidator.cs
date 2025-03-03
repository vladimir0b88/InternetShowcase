using FluentValidation;

namespace Application.Models
{
    public class ProductAddDtoValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddDtoValidator(IProductTypeRepository productTypeRepository)
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);

            RuleFor(p => p.TypeId).Custom(async (typeId, context) =>
            {
                if (typeId is null)
                    return;

                bool typeExist = await productTypeRepository.ExistByIdAsync((long)typeId);

                if (!typeExist)
                    context.AddFailure($"Указанный тип продукта с id: {typeId} не существует");

            });
        }
    }
}
