using Application.Extensions;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class ProductService(IProductRepository productRepository,
                                ITypePropertyService typePropertyService,
                                IValidator<ProductAddDto> createValidator,
                                IValidator<ProductUpdateDto> updateValidator,
                                IValidator<ProductsFilter> filterValidator) : IProductService
    {
        public async Task<ErrorOr<Product>> GetByIdAsync(long id)
        {
            var result = await productRepository.GetByIdAsync(id);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            var result = await productRepository.DeleteByIdAsync(id);

            return result;
        }

        public async Task<ErrorOr<Created>> AddAsync(ProductAddDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            Product newProduct = productDto.ToEntity();
               
            var insertResult = await productRepository.InsertAsync(newProduct);

            if (insertResult.IsError)
                return insertResult.Errors;



            newProduct = insertResult.Value;

            if (newProduct.TypeId is null)
                return Result.Created;

            var result = await typePropertyService.AddPropertiesValuesForProductAsync(newProduct);

            return result;
        }

        public async Task<ErrorOr<List<Product>>> GetAllAsync()
        {
            var result = await productRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            Product product = updateDto.ToEntity();

            var result = await productRepository.UpdateAsync(product);

            return result;
        }

        public async Task<ErrorOr<List<Product>>> GetByProductTypeIdAsync(long productTypeId)
        {
            var result = await productRepository.GetByProductTypeIdAsync(productTypeId);

            return result;
        }

        public async Task<ErrorOr<ProductsFilteringResult>> GetByFilterAsync(ProductsFilter filter)
        {
            var validationResult = await filterValidator.ValidateAsync(filter);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            var result = await productRepository.GetByFilterAsync(filter);

            return result;
        }
    }
}
