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
    [Route("[controller]")]
    public class ProductTypesController(IProductTypeService productTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var result = await productTypeService.GetAllProductTypes();

            return result switch
            {
                SuccessResult<List<ProductType>> => Ok(result.Data),
                ErrorResult<List<ProductType>> errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var result = await productTypeService.GetProductTypeById(id);

            return result switch
            {
                SuccessResult<ProductType> => Ok(result.Data),
                NotFoundErrorResult<ProductType> errorResult => BadRequest(errorResult),
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
                ValidationErrorResult errorResult => BadRequest(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductType([FromBody] ProductTypeUpdateDto updateDto)
        {
            var result = await productTypeService.UpdateProductType(updateDto);

            return result switch
            {
                SuccessResult => Ok(),
                ValidationErrorResult errorResult => BadRequest(errorResult),
                NotFoundErrorResult errorResult => NotFound(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(long id)
        {
            var result = await productTypeService.DeleteProductTypeById(id);

            return result switch
            {
                SuccessResult => Ok(),
                NotFoundErrorResult errorResult => NotFound(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }
    }
}
