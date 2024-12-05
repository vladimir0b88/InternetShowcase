using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class TypePropertyService(ITypePropertyRepository repository,
                                     IValidator<TypePropertyCreateDto> createValidator,
                                     IValidator<TypePropertyUpdateDto> updateValidator) : ITypePropertyService
    {
        public async Task<ErrorOr<List<TypeProperty>>> GetAllTypeProperties()
        {
            var result = await repository.GetAllTypeProperties();

            return result;
        }

        public async Task<ErrorOr<Created>> AddProperty(TypePropertyCreateDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


            TypeProperty newProperty = new TypeProperty()
            {
                Name = createDto.Name,
                TypeId = createDto.TypeId,
            };

            var result = await repository.AddProperty(newProperty);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteProperty(long propertyId)
        {
            var result = await repository.DeleteProperty(propertyId);

            return result;
        }


        public async Task<ErrorOr<List<TypeProperty>>> GetPropertiesByProductTypeId(long typeId)
        {
            var result = await repository.GetPropertiesByTypeId(typeId);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateProperty(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if(!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


            TypeProperty property = new TypeProperty()
            {
                Id = updateDto.Id,
                Name = updateDto.Name
            };

            var result = await repository.UpdateProperty(property);

            return result;
        }

        public async Task<ErrorOr<TypeProperty>> GetPropertyById(long propertyId)
        {
            var result = await repository.GetPropertyById(propertyId);

            return result;
        }
    }
}
