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
            var result = await productTypeService.GetAllAsync();

            return this.SendResponse(result);
        }


        [HttpGet("{typeId}")]
        public async Task<ActionResult<ProductType>> GetProductTypeById(long typeId)
        {
            var result = await productTypeService.GetByIdAsync(typeId);

            return this.SendResponse(result);
        }


        [Authorize(Policy = Policies.CanCreate)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddProductType([FromBody] ProductTypeAddDto createDto)
        {
            var result = await productTypeService.AddAsync(createDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateProductType([FromBody] ProductTypeUpdateDto updateDto)
        {
            var result = await productTypeService.UpdateAsync(updateDto);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{typeId}")]
        public async Task<ActionResult<Deleted>> DeleteProductById(long typeId)
        {
            var result = await productTypeService.DeleteByIdAsync(typeId);

            return this.SendResponse(result);
        }
    }
}
