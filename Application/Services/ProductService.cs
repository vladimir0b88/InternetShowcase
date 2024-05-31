using Application.Common;
using Application.Models;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class ProductService(IProductRepository repository, 
                                IValidator<ProductCreateDto> createValidator,
                                IValidator<ProductUpdateDto> updateValidator) : IProductService
    {
        public async Task<Result<Product>> GetProductById(long id)
        {
            var result = await repository.GetProductById(id);

            return result;
        }

        public async Task<Result> DeleteProductById(long id)
        {
            var result = await repository.DeleteProductById(id);

            return result;
        }

        public async Task<Result> AddProduct(ProductCreateDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для создания не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            Product newProduct = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Cost = productDto.Cost,
                TypeId = productDto.TypeId,
            };

            var result  = await repository.AddProduct(newProduct);

            return result;
        }

        public async Task<Result<List<Product>>> GetAllProducts()
        {
            var result = await repository.GetAll();

            return result;
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для модификации не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            Product product = new Product()
            {
                Id = updateDto.Id,
                Name = updateDto.Name,
                Description = updateDto.Description,
                Cost = updateDto.Cost,
                TypeId = updateDto.TypeId,
            };

            var result = await repository.UpdateProduct(product);

            return result;
        }

        public async Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            var result = await repository.GetByProductTypeId(productTypeId);

            return result;
        }
    }
}
