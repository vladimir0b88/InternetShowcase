using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var result = await productService.GetAllProducts();

            return this.SendResponse(result);
        }

        [HttpGet("ProductType/{id}")]
        public async Task<ActionResult<List<Product>>> GetProductsByProductTypeId(long id)
        {
            var result = await productService.GetByProductTypeId(id);

            return this.SendResponse(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(long id)
        {
            var result = await productService.GetProductById(id);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddProduct([FromBody] ProductCreateDto createDto)
        {
            var result = await productService.AddProduct(createDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateProduct([FromBody] ProductUpdateDto updateDto)
        {
            var result = await productService.UpdateProduct(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteProductById(long id)
        {
            var result = await productService.DeleteProductById(id);

            return this.SendResponse(result);
        }

        [HttpPost("Filter")]
        public async Task<ActionResult<FilteringResult<Product>>> GetProductsByFilter(ProductsFilter filter)
        {
            var result = await productService.GetProductsByFilter(filter);

            return this.SendResponse(result);
        }
    }
}
