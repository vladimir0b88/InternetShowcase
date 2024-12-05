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
    public class TypePropertiesController(ITypePropertyService propertyService) : ControllerBase
    {
        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<TypeProperty>>> GetAllTypeProperties()
        {
            var result = await propertyService.GetAllTypeProperties();

            return this.SendResponse(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<TypeProperty>> GetPropertyById(long propertyId)
        {
            var result = await propertyService.GetPropertyById(propertyId);

            return this.SendResponse(result);
        }

        [HttpGet("ProductType/{id}")]
        public async Task<ActionResult<List<TypeProperty>>> GetPropertiesByProductTypeId(long id)
        {
            var result = await propertyService.GetPropertiesByProductTypeId(id);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddTypeProperty([FromBody]TypePropertyCreateDto createDto)
        {
            var result = await propertyService.AddProperty(createDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateTypeProperty([FromBody]TypePropertyUpdateDto updateDto)
        {
            var result = await propertyService.UpdateProperty(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteTypeProperty(long id)
        {
            var result = await propertyService.DeleteProperty(id);

            return this.SendResponse(result);
        }

    }
}
