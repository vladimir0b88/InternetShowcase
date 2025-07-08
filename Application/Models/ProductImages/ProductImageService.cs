using Application.Extensions;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class ProductImageService(IProductImageRepository repository,
                                     IValidator<ProductImageAddDto> addValidator) : IProductImageService
    {
        public async Task<ErrorOr<Created>> AddAsync(ProductImageAddDto addDto)
        {
            var validationResult = await addValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();


            ProductImage productImage = addDto.ToEntity();
            
            var result = await repository.InsertAsync(productImage);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            var result = await repository.DeleteByIdAsync(id);

            return result;
        }

        public async Task<ErrorOr<ProductImage>> GetFirstByProductIdAsync(long productId)
        {
            var result = await repository.GetFirstByProductIdAsync(productId);

            return result;
        }

        public async Task<ErrorOr<ProductImage>> GetByIdAsync(long imageId)
        {
            var result = await repository.GetByIdAsync(imageId);

            return result;
        }

        public async Task<ErrorOr<List<ProductImage>>> GetAllByProductIdAsync(long productId)
        {
            var result = await repository.GetAllByProductIdAsync(productId);

            return result;
        }
    }
}
