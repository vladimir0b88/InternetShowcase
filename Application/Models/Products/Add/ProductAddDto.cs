using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class ProductAddDto
    {
        public string Name { get; set; } = string.Empty;

        public long Cost { get; set; }

        public long? TypeId { get; set; } = null;

        public string Description { get; set; } = string.Empty;
    }



    public static class ProductsAddMapper
    {
        public static Product ToEntity(this ProductAddDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Cost = dto.Cost,
                TypeId = dto.TypeId,
                Description = dto.Description,
            };
        }
    }


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
