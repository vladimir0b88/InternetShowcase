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
    public class PropertyValuesController(IPropertyValueService propertyValueService) : ControllerBase
    {
        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<PropertyValue>>> GetAllPropertyValues()
        {
            var result = await propertyValueService.GetAllPropertyValues();

            return this.SendResponse(result);
        }


        [HttpGet("ProductType/{id}/UniqueValues")]
        public async Task<ActionResult<List<UniquePropertyValues>>> GetUniquePropertyValuesByProductTypeId(long id)
        {
            var result = await propertyValueService.GetUniquePropertyValues(id);

            return this.SendResponse(result);
        }


        [HttpGet("Product/{id}")]
        public async Task<ActionResult<List<PropertyValue>>> GetPropertyValuesByProductId(long id)
        {
            var result = await propertyValueService.GetPropertyValuesByProductId(id);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdatePropertyValue([FromBody]PropertyValueUpdateDto updateDto)
        {
            var result = await propertyValueService.UpdatePropertyValue(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut("List")]
        public async Task<ActionResult<Updated>> UpdatePropertyValueList([FromBody] PropertyValueUpdateDtoList updateDtoList)
        {
            var result = await propertyValueService.UpdatePropertyValueList(updateDtoList);

            return this.SendResponse(result);
        }

    }
}
