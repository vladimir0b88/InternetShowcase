using Application.Common;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class ProductService(IProductRepository repository,
                                IValidator<ProductCreateDto> createValidator,
                                IValidator<ProductUpdateDto> updateValidator,
                                IValidator<ProductsFilter> filterValidator) : IProductService
    {
        public async Task<ErrorOr<Product>> GetProductById(long id)
        {
            var result = await repository.GetProductById(id);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteProductById(long id)
        {
            var result = await repository.DeleteProductById(id);

            return result;
        }

        public async Task<ErrorOr<Created>> AddProduct(ProductCreateDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

            Product newProduct = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Cost = productDto.Cost,
                TypeId = productDto.TypeId,
            };

            var result = await repository.AddProduct(newProduct);

            return result;
        }

        public async Task<ErrorOr<List<Product>>> GetAllProducts()
        {
            var result = await repository.GetAll();

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateProduct(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

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

        public async Task<ErrorOr<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            var result = await repository.GetByProductTypeId(productTypeId);

            return result;
        }

        public async Task<ErrorOr<FilteringResult<Product>>> GetProductsByFilter(ProductsFilter filter)
        {
            var validationResult = await filterValidator.ValidateAsync(filter);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

            var result = await repository.GetByFilter(filter);

            return result;
        }
    }
}
