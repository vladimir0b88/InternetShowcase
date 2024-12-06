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
    public class TypePropertiesController(ITypePropertyService propertyService) : ControllerBase
    {
        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<TypeProperty>>> GetAllTypeProperties()
        {
            var result = await propertyService.GetAllAsync();

            return this.SendResponse(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<TypeProperty>> GetPropertyById(long propertyId)
        {
            var result = await propertyService.GetByIdAsync(propertyId);

            return this.SendResponse(result);
        }

        [HttpGet("ProductType/{typeId}")]
        public async Task<ActionResult<List<TypeProperty>>> GetPropertiesByProductTypeId(long typeId)
        {
            var result = await propertyService.GetByProductTypeIdAsync(typeId);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddTypeProperty([FromBody]TypePropertyAddDto createDto)
        {
            var result = await propertyService.AddAsync(createDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateTypeProperty([FromBody]TypePropertyUpdateDto updateDto)
        {
            var result = await propertyService.UpdateAsync(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteTypeProperty(long id)
        {
            var result = await propertyService.DeleteAsync(id);

            return this.SendResponse(result);
        }

    }
}
