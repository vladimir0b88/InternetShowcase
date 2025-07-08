
using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class TypePropertyAddDto
    {
        public string Name { get; set; } = string.Empty;
        public long? TypeId { get; set; }
    }


    public static class TypePropertyAddMapper
    {
        public static ProductTypeProperty ToEntity(this TypePropertyAddDto dto)
        {
            return new ProductTypeProperty
            {
                Name = dto.Name,
                TypeId = dto.TypeId,
            };
        }

    }

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
