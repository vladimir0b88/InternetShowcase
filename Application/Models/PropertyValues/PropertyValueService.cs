using Application.Common;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class PropertyValueService(IPropertyValueRepository repository,
                                      IValidator<PropertyValueUpdateDto> updateValidator,
                                      IValidator<PropertyValueUpdateDtoList> listUpdateValidator) : IPropertyValueService
    {
        public async Task<ErrorOr<List<PropertyValue>>> GetAllPropertyValues()
        {
            var result = await repository.GetAllPropertyValues();

            return result;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetPropertyValuesByProductId(long productId)
        {
            var result = await repository.GetPropertyValuesByProductId(productId);

            return result;
        }

        public async Task<ErrorOr<List<UniquePropertyValues>>> GetUniquePropertyValues(long productTypeId)
        {
            var result = await repository.GetUniquePropertyValues(productTypeId);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdatePropertyValue(PropertyValueUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


            PropertyValue propertyValue = new PropertyValue()
            {
                Id = updateDto.Id,
                Value = updateDto.Value,
            };

            var result = await repository.UpdatePropertyValue(propertyValue);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdatePropertyValueList(PropertyValueUpdateDtoList updateDtoList)
        {
            var validationResult = await listUpdateValidator.ValidateAsync(updateDtoList);

            if(!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


            List<Error> errors = [];

            foreach (var updateDto in updateDtoList.List)
            {
                PropertyValue propertyValue = new()
                {
                    Id = updateDto.Id,
                    Value = updateDto.Value,
                };

                var tempResult = await repository.UpdatePropertyValue(propertyValue);

                if (tempResult.IsError)
                    errors.AddRange(tempResult.Errors);
            }

            if(errors.Count > 0)
                return errors;

            return Result.Updated;
        }
    }
}
