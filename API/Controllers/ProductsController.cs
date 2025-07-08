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
            var result = await productService.GetAllAsync();

            return this.SendResponse(result);
        }

        [HttpGet("ProductType/{typeId}")]
        public async Task<ActionResult<List<Product>>> GetProductsByProductTypeId(long typeId)
        {
            var result = await productService.GetByProductTypeIdAsync(typeId);

            return this.SendResponse(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(long id)
        {
            var result = await productService.GetByIdAsync(id);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddProduct([FromBody] ProductAddDto createDto)
        {
            HttpContext context = HttpContext;

            var result = await productService.AddAsync(createDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateProduct([FromBody] ProductUpdateDto updateDto)
        {
            var result = await productService.UpdateAsync(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteProductById(long id)
        {
            var result = await productService.DeleteByIdAsync(id);

            return this.SendResponse(result);
        }

        [HttpPost("Filter")]
        public async Task<ActionResult<ProductsFilteringResult>> GetProductsByFilter([FromBody]ProductsFilter filter)
        {
            var result = await productService.GetByFilterAsync(filter);

            return this.SendResponse(result);
        }
    }
}
