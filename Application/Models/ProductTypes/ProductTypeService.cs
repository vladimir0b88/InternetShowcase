using Application.Common;
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

        public async Task<ErrorOr<Created>> AddAsync(ProductTypeAddDto dto)
        {
            var validationResult = await createDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductType productType = new ProductType() 
            {
                Name = dto.Name,
            };

            var result = await productTypeRepository.InsertAsync(productType);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            var result = await productTypeRepository.DeleteByIdAsync(id);

            return result;
        }


        public async Task<ErrorOr<Updated>> UpdateAsync(ProductTypeUpdateDto dto)
        {
            var validationResult = await updateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductType productType = new ProductType()
            {
                Id = dto.Id,
                Name = dto.Name,
            };

            var result = await productTypeRepository.UpdateAsync(productType);

            return result;
        }
    }
}
