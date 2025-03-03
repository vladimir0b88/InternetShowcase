using Application.Extensions;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class TypePropertyService(ITypePropertyRepository TypePropertyRepository,
                                     IProductRepository productRepository,
                                     IValidator<TypePropertyAddDto> createValidator,
                                     IValidator<TypePropertyUpdateDto> updateValidator) : ITypePropertyService
    {
        public async Task<ErrorOr<List<TypeProperty>>> GetAllAsync()
        {
            var result = await TypePropertyRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<Created>> AddAsync(TypePropertyAddDto addDto)
        {
            var validationResult = await createValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            TypeProperty newProperty = addDto.ToEntity();

            var result = await TypePropertyRepository.InsertAsync(newProperty);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(long propertyId)
        {
            var result = await TypePropertyRepository.DeleteAsync(propertyId);

            return result;
        }


        public async Task<ErrorOr<List<TypeProperty>>> GetByProductTypeIdAsync(long typeId)
        {
            var result = await TypePropertyRepository.GetByTypeIdAsync(typeId);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            TypeProperty property = updateDto.ToEntity();

            var result = await TypePropertyRepository.UpdateAsync(property);

            return result;
        }

        public async Task<ErrorOr<TypeProperty>> GetByIdAsync(long propertyId)
        {
            var result = await TypePropertyRepository.GetByIdAsync(propertyId);

            return result;
        }

        public async Task<ErrorOr<Created>> AddPropertiesValuesForProductAsync(Product product)
        {
            if (product.Id == 0 ||
                product.TypeId is null)
                return Error.Validation(description: "Ошибка добавления характеристик продукту. " +
                                                     "Указан пустой тип продукта или передан продукт без Id");

            throw new();

        }
    }
}
