using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class ProductImageService(IProductImageRepository repository,
                                     IValidator<ProductImageAddDto> addValidator) : IProductImageService
    {
        public async Task<ErrorOr<Created>> AddImage(ProductImageAddDto addDto)
        {
            var validationResult = await addValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

            ProductImage productImage = new ProductImage()
            {
                ProductId = addDto.ProductId,
                Format = addDto.Format,
                Image = addDto.Image,
            };

            var result = await repository.AddImage(productImage);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteImage(long id)
        {
            var result = await repository.DeleteImage(id);

            return result;
        }

        public async Task<ErrorOr<ProductImage>> GetFirstImageByProductId(long productId)
        {
            var result = await repository.GetFirstImageByProductId(productId);

            return result;
        }

        public async Task<ErrorOr<ProductImage>> GetImageById(long imageId)
        {
            var result = await repository.GetImageById(imageId);

            return result;
        }

        public async Task<ErrorOr<List<ProductImage>>> GetImagesByProductId(long productId)
        {
            var result = await repository.GetImagesByProductId(productId);

            return result;
        }
    }
}
