using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class TypePropertyService(ITypePropertyRepository repository,
                                     IValidator<TypePropertyCreateDto> createValidator,
                                     IValidator<TypePropertyUpdateDto> updateValidator) : ITypePropertyService
    {
        public async Task<Result> AddProperty(TypePropertyCreateDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Свойство типа товара не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationResult.Errors);

            TypeProperty newProperty = new TypeProperty()
            {
                Name = createDto.Name,
                TypeId = createDto.TypeId,
            };

            var result = await repository.AddProperty(newProperty);

            return result;
        }

        public async Task<Result> DeleteProperty(long propertyId)
        {
            var result = await repository.DeleteProperty(propertyId);

            return result;
        }

        public async Task<Result<List<TypeProperty>>> GetPropertiesByTypeId(long typeId)
        {
            var result = await repository.GetPropertiesByTypeId(typeId);

            return result;
        }

        public async Task<Result> UpdateProperty(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if(!validationResult.IsValid)
                return new ValidationErrorResult(message: "Свойство типа товара для изменения не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation], 
                                                 validationResult.Errors);

            TypeProperty property = new TypeProperty()
            {
                Id = updateDto.Id,
                Name = updateDto.Name
            };

            var result = await repository.UpdateProperty(property);

            return result;
        }
    }
}
