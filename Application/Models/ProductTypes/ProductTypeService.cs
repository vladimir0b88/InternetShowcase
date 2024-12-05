using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    public class ProductTypeService (IProductTypeRepository repository,
                                     IValidator<ProductTypeCreateDto> createDtoValidator,
                                     IValidator<ProductTypeUpdateDto> updateDtoValidator) : IProductTypeService
    {
        public async Task<ErrorOr<List<ProductType>>> GetAllProductTypes()
        {
            var result = await repository.GetAllProductTypes();

            return result;
        }

        public async Task<ErrorOr<ProductType>> GetProductTypeById(long id)
        {
            var result = await repository.GetProductTypeById(id);

            return result;
        }

        public async Task<ErrorOr<Created>> AddProductType(ProductTypeCreateDto dto)
        {
            var validationResult = await createDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


            ProductType productType = new ProductType() 
            {
                Name = dto.Name,
            };

            var result = await repository.AddProductType(productType);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteProductTypeById(long id)
        {
            var result = await repository.DeleteProductTypeById(id);

            return result;
        }


        public async Task<ErrorOr<Updated>> UpdateProductType(ProductTypeUpdateDto dto)
        {
            var validationResult = await updateDtoValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
               return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

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
