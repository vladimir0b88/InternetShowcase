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
            var result = await propertyValueService.GetAllAsync();

            return this.SendResponse(result);
        }


        [HttpGet("ProductType/{typeId}/UniqueValues")]
        public async Task<ActionResult<List<UniquePropertyValues>>> GetUniquePropertyValuesByProductTypeId(long typeId)
        {
            var result = await propertyValueService.GetUniquesByProductTypeIdAsync(typeId);

            return this.SendResponse(result);
        }


        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<List<PropertyValue>>> GetPropertyValuesByProductId(long productId)
        {
            var result = await propertyValueService.GetByProductIdAsync(productId);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdatePropertyValue([FromBody]PropertyValueUpdateDto updateDto)
        {
            var result = await propertyValueService.UpdateAsync(updateDto);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpPut("List")]
        public async Task<ActionResult<Updated>> UpdatePropertyValueList([FromBody] PropertyValueUpdateDtoList updateDtoList)
        {
            var result = await propertyValueService.UpdateListAsync(updateDtoList);

            return this.SendResponse(result);
        }

    }
}
