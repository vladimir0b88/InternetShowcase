using Application.Common;
using Application.Models;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class ProductImageService(IProductImageRepository repository,
                                     IValidator<ProductImageAddDto> addValidator) : IProductImageService
    {
        public async Task<Result> AddImage(ProductImageAddDto addDto)
        {
            var validationResult = await addValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Добавляемое изображение не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            ProductImage productImage = new ProductImage()
            {
                ProductId = addDto.ProductId,
            };

            using (MemoryStream ms = new MemoryStream())
            {
                addDto.FormFile.CopyTo(ms);
                productImage.Image = ms.ToArray();
            }

            var result = await repository.AddImage(productImage);

            return result;
        }

        public async Task<Result> DeleteImage(long id)
        {
            var result = await repository.DeleteImage(id);

            return result;
        }

        public async Task<Result<ProductImage>> GetImageById(long imageId)
        {
            var result = await repository.GetImageById(imageId);

            return result;
        }

        public async Task<Result<List<ProductImage>>> GetImagesByProductId(long productId)
        {
            var result = await repository.GetImagesByProductId(productId);

            return result;
        }
    }
}
