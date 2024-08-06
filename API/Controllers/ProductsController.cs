using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productService.GetAllProducts();

            return result switch
            {
                SuccessResult<List<Product>> => Ok(result),
                ErrorResult<List<Product>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpGet("ProductType/{id}")]
        public async Task<IActionResult> GetProductsByProductTypeId(long id)
        {
            var result = await productService.GetByProductTypeId(id);

            return result switch
            {
                SuccessResult<List<Product>> => Ok(result),
                NotFoundErrorResult<List<Product>> => NotFound(result),
                ErrorResult<List<Product>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var result = await productService.GetProductById(id);

            return result switch
            {
                SuccessResult<Product> => Ok(result),
                NotFoundErrorResult<Product> => NotFound(result),
                _ => throw new ApplicationException()
            };
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto createDto)
        {
            var result = await productService.AddProduct(createDto);

            return result switch
            {
                SuccessResult => Created(),
                ValidationErrorResult => StatusCode(422,result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto updateDto)
        {
            var result = await productService.UpdateProduct(updateDto);

            return result switch
            {
                SuccessResult => Ok(result),
                ValidationErrorResult => StatusCode(422,result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(long id)
        {
            var result = await productService.DeleteProductById(id);

            return result switch
            {
                SuccessResult => Ok(result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> GetProductsByFilter(ProductsFilter filter)
        {
            var result = await productService.GetProductsByFilter(filter);

            return result switch
            {
                SuccessResult<FilteringResult<Product>> => Ok(result),
                ValidationErrorResult<FilteringResult<Product>> => StatusCode(422, result),
                NotFoundErrorResult<FilteringResult<Product>> => NotFound(result),
                ErrorResult<FilteringResult<Product>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }
    }
}
