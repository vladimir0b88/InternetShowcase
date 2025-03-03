using Application.Extensions;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class ProductTypeService (IProductTypeRepository productTypeRepository,
                                     IValidator<ProductTypeAddDto> createDtoValidator,
                                     IValidator<ProductTypeUpdateDto> updateDtoValidator) : IProductTypeService
    {
        public async Task<ErrorOr<List<ProductType>>> GetAllAsync()
        {
            var result = await productTypeRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<ProductType>> GetByIdAsync(long id)
        {
            var result = await productTypeRepository.GetByIdAsync(id);

            return result;
        }

        public async Task<ErrorOr<Created>> AddAsync(ProductTypeAddDto addDto)
        {
            var validationResult = await createDtoValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductType productType = addDto.ToEntity();

            var result = await productTypeRepository.InsertAsync(productType);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            var result = await productTypeRepository.DeleteByIdAsync(id);

            return result;
        }


        public async Task<ErrorOr<Updated>> UpdateAsync(ProductTypeUpdateDto updateDto)
        {
            var validationResult = await updateDtoValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductType productType = updateDto.ToEntity();

            var result = await productTypeRepository.UpdateAsync(productType);

            return result;
        }
    }
}
