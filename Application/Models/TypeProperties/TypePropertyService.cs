using Application.Common;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class TypePropertyService(ITypePropertyRepository repository,
                                     IValidator<TypePropertyAddDto> createValidator,
                                     IValidator<TypePropertyUpdateDto> updateValidator) : ITypePropertyService
    {
        public async Task<ErrorOr<List<TypeProperty>>> GetAllAsync()
        {
            var result = await repository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<Created>> AddAsync(TypePropertyAddDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            TypeProperty newProperty = new TypeProperty()
            {
                Name = createDto.Name,
                TypeId = createDto.TypeId,
            };

            var result = await repository.InsertAsync(newProperty);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(long propertyId)
        {
            var result = await repository.DeleteAsync(propertyId);

            return result;
        }


        public async Task<ErrorOr<List<TypeProperty>>> GetByProductTypeIdAsync(long typeId)
        {
            var result = await repository.GetByTypeIdAsync(typeId);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            TypeProperty property = new TypeProperty()
            {
                Id = updateDto.Id,
                Name = updateDto.Name
            };

            var result = await repository.UpdateAsync(property);

            return result;
        }

        public async Task<ErrorOr<TypeProperty>> GetByIdAsync(long propertyId)
        {
            var result = await repository.GetByIdAsync(propertyId);

            return result;
        }
    }
}
