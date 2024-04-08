using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productService.GetAllProducts();

            return result switch
            {
                SuccessResult<List<Product>> => Ok(result.Data),
                ErrorResult<List<Product>> errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var result = await productService.GetProductById(id);

            return result switch
            {
                SuccessResult<Product> => Ok(result.Data),
                NotFoundErrorResult<Product> notFoundResult => NotFound(notFoundResult),
                _ => throw new ApplicationException()
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]ProductCreateDto createDto)
        {
            var result = await productService.AddProduct(createDto);

            return result switch
            {
                SuccessResult => Created(),
                ValidationErrorResult errorResult => BadRequest(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto updateDto)
        {
            var result = await productService.UpdateProduct(updateDto);

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
            var result = await productService.DeleteProductById(id);

            return result switch
            {
                SuccessResult => Ok(),
                NotFoundErrorResult errorResult => NotFound(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }

        [HttpGet("ProductType/{id}")]
        public async Task<IActionResult> GetProductsByProductTypeId(long id)
        {
            var result = await productService.GetByProductTypeId(id);

            return result switch
            {
                SuccessResult<List<Product>> => Ok(result.Data),
                NotFoundErrorResult<List<Product>> errorResult => NotFound(errorResult),
                ErrorResult<List<Product>> errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }
    }
}
