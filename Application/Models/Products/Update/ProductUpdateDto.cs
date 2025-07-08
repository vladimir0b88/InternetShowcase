using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class ProductUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long Cost { get; set; }
        public long? TypeId { get; set; } = null;

        public string Description { get; set; } = string.Empty;
    }


    public static class ProductUpdateMapper
    {
        public static Product ToEntity(this ProductUpdateDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Cost = dto.Cost,
                TypeId = dto.TypeId,
                Description = dto.Description,
            };
        }
    }


    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator(IProductTypeRepository productTypeRepository)
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);

            RuleFor(p => p.TypeId).Custom(async (typeId, context) =>
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
