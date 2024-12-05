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
    public class ProductTypesController(IProductTypeService productTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> GetAllProductTypes()
        {
            var result = await productTypeService.GetAllProductTypes();

            return this.SendResponse(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductTypeById(long id)
        {
            var result = await productTypeService.GetProductTypeById(id);

            return this.SendResponse(result);
        }


        [Authorize(Policy = Policies.CanCreate)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddProductType([FromBody] ProductTypeCreateDto createDto)
        {
            var result = await productTypeService.AddProductType(createDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateProductType([FromBody] ProductTypeUpdateDto updateDto)
        {
            var result = await productTypeService.UpdateProductType(updateDto);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteProductById(long id)
        {
            var result = await productTypeService.DeleteProductTypeById(id);

            return this.SendResponse(result);
        }
    }
}
