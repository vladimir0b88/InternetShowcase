using Application.Common;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class PropertyValueService(IPropertyValueRepository propertyValueRepository,
                                      IValidator<PropertyValueUpdateDto> updateValidator,
                                      IValidator<PropertyValueUpdateDtoList> listUpdateValidator) : IPropertyValueService
    {
        public async Task<ErrorOr<List<PropertyValue>>> GetAllAsync()
        {
            var result = await propertyValueRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetByProductIdAsync(long productId)
        {
            var result = await propertyValueRepository.GetByProductIdAsync(productId);

            return result;
        }

        public async Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesByProductTypeIdAsync(long productTypeId)
        {
            var result = await propertyValueRepository.GetUniquesByProductTypeIdAsync(productTypeId);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(PropertyValueUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            PropertyValue propertyValue = new PropertyValue()
            {
                Id = updateDto.Id,
                Value = updateDto.Value,
            };

            var result = await propertyValueRepository.UpdateAsync(propertyValue);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateListAsync(PropertyValueUpdateDtoList updateDtoList)
        {
            var validationResult = await listUpdateValidator.ValidateAsync(updateDtoList);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            List<Error> errors = [];

            foreach (var updateDto in updateDtoList.List)
            {
                PropertyValue propertyValue = new()
                {
                    Id = updateDto.Id,
                    Value = updateDto.Value,
                };

                var tempResult = await propertyValueRepository.UpdateAsync(propertyValue);

                if (tempResult.IsError)
                    errors.AddRange(tempResult.Errors);
            }

            if(errors.Count > 0)
                return errors;

            return Result.Updated;
        }
    }
}
