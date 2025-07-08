using Application.Common;
using Application.Extensions;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class PropertyService(ITypePropertyRepository typePropertyRepository,
                                 IPropertyValueRepository propertyValueRepository,
                                 IValidator<TypePropertyAddDto> createValidator,
                                 IValidator<TypePropertyUpdateDto> propertyUpdateValidator,
                                 IValidator<PropertyValueUpdateDto> PVUpdateValidator,
                                 IValidator<PropertyValueUpdateDtoList> listPVUpdateValidator) : IPropertyService
    {
        // Create
        public async Task<ErrorOr<Created>> AddPropertyAsync(TypePropertyAddDto addDto)
        {
            var validationResult = await createValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductTypeProperty newProperty = addDto.ToEntity();

            var result = await typePropertyRepository.InsertAsync(newProperty);

            return result;
        }

        public async Task<ErrorOr<Created>> AddPropertiesValuesForProductAsync(Product product)
        {
            if (product.Id == 0 ||
                product.TypeId is null)
                return Error.Validation(description: "Ошибка добавления характеристик продукту. " +
                                                     "Указан пустой тип продукта или передан продукт без Id");

            var result = await typePropertyRepository.AddPropertiesValuesForProductAsync(product);

            return result;
        }


        // Change
        public async Task<ErrorOr<Updated>> UpdatePropertyAsync(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await propertyUpdateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductTypeProperty property = updateDto.ToEntity();

            var result = await typePropertyRepository.UpdateAsync(property);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdatePropertyValueAsync(PropertyValueUpdateDto updateDto)
        {
            var validationResult = await PVUpdateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            PropertyValue propertyValue = updateDto.ToEntity();

            var result = await propertyValueRepository.UpdateAsync(propertyValue);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdatePropertyValuesListAsync(PropertyValueUpdateDtoList updateDtoList)
        {
            var validationResult = await listPVUpdateValidator.ValidateAsync(updateDtoList);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            List<Error> errors = [];

            foreach (var updateDto in updateDtoList.List)
            {
                PropertyValue propertyValue = updateDto.ToEntity();

                var tempResult = await propertyValueRepository.UpdateAsync(propertyValue);

                if (tempResult.IsError)
                    errors.AddRange(tempResult.Errors);
            }

            if (errors.Count > 0)
                return errors;

            return Result.Updated;
        }

        // Delete
        public async Task<ErrorOr<Deleted>> DeletePropertyAsync(long propertyId)
        {
            var result = await typePropertyRepository.DeleteAsync(propertyId);

            return result;
        }

        // Get
        public async Task<ErrorOr<List<ProductTypeProperty>>> GetAllPropertiesAsync()
        {
            var result = await typePropertyRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<List<ProductTypeProperty>>> GetPropertyByProductTypeIdAsync(long typeId)
        {
            var result = await typePropertyRepository.GetByTypeIdAsync(typeId);

            return result;
        }

        public async Task<ErrorOr<ProductTypeProperty>> GetPropertyByIdAsync(long propertyId)
        {
            var result = await typePropertyRepository.GetByIdAsync(propertyId);

            return result;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetAllPropertyValuesAsync()
        {
            var result = await propertyValueRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetPropertyValuesByProductIdAsync(long productId)
        {
            var result = await propertyValueRepository.GetByProductIdAsync(productId);

            return result;
        }

        public async Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesPropertyValuesByProductTypeIdAsync(long productTypeId)
        {
            var result = await propertyValueRepository.GetUniquesByProductTypeIdAsync(productTypeId);

            return result;
        }


    }
}
