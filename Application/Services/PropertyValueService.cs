using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.PropertyValues.Update;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class PropertyValueService(IPropertyValueRepository repository,
                                      IValidator<PropertyValueUpdateDto> updateValidator) : IPropertyValueService
    {
        
        public async Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId)
        {
            var result = await repository.GetPropertyValuesByProductId(productId);

            return result;
        }

        public async Task<Result> UpdatePropertyValue(PropertyValueUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Значение свойства не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            PropertyValue propertyValue = new PropertyValue()
            {
                Id = updateDto.Id,
                Value = updateDto.Value,
            };

            var result = await repository.UpdatePropertyValue(propertyValue);

            return result;
        }
    }
}
