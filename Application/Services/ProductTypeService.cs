
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class ProductTypeService (IProductTypeRepository repository,
                                     IValidator<ProductTypeCreateDto> createDtoValidator,
                                     IValidator<ProductTypeUpdateDto> updateDtoValidator) : IProductTypeService
    {
        public async Task<Result<List<ProductType>>> GetAllProductTypes()
        {
            var result = await repository.GetAllProductTypes();

            return result;
        }

        public async Task<Result<ProductType>> GetProductTypeById(long id)
        {
            var result = await repository.GetProductTypeById(id);

            return result;
        }

        public async Task<Result> AddProductType(ProductTypeCreateDto dto)
        {
            var validationResult = await createDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Тип продукта для создания не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            ProductType productType = new ProductType() 
            {
                Name = dto.Name,
            };

            var result = await repository.AddProductType(productType);

            return result;
        }

        public async Task<Result> DeleteProductTypeById(long id)
        {
            var result = await repository.DeleteProductTypeById(id);

            return result;
        }


        public async Task<Result> UpdateProductType(ProductTypeUpdateDto dto)
        {
            var validationResult = await updateDtoValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return new ValidationErrorResult(message: "Тип продукта для изменения не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            ProductType productType = new ProductType()
            {
                Id = dto.Id,
                Name = dto.Name,
            };

            var result = await repository.UpdateProductType(productType);

            return result;
        }
    }
}
