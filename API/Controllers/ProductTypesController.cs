using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController(IProductTypeService productTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var result = await productTypeService.GetAllProductTypes();

            return result switch
            {
                SuccessResult<List<ProductType>> => Ok(result),
                ErrorResult<List<ProductType>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductTypeById(long id)
        {
            var result = await productTypeService.GetProductTypeById(id);

            return result switch
            {
                SuccessResult<ProductType> => Ok(result),
                NotFoundErrorResult<ProductType> => NotFound(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType([FromBody] ProductTypeCreateDto createDto)
        {
            var result = await productTypeService.AddProductType(createDto);

            return result switch
            {
                SuccessResult => Created(),
                ValidationErrorResult => StatusCode(422, result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductType([FromBody] ProductTypeUpdateDto updateDto)
        {
            var result = await productTypeService.UpdateProductType(updateDto);

            return result switch
            {
                SuccessResult => Ok(result),
                ValidationErrorResult => StatusCode(422, result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(long id)
        {
            var result = await productTypeService.DeleteProductTypeById(id);

            return result switch
            {
                SuccessResult => Ok(result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }
    }
}
