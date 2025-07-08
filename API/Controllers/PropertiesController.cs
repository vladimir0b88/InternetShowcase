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
    public class PropertiesController(IPropertyService propertyService) : ControllerBase
    {
        #region Create=============================================

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> AddTypeProperty([FromBody] TypePropertyAddDto createDto)
        {
            var result = await propertyService.AddPropertyAsync(createDto);

            return this.SendResponse(result);
        }
        #endregion


        #region Change==============================================

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<ActionResult<Updated>> UpdateTypeProperty([FromBody] TypePropertyUpdateDto updateDto)
        {
            var result = await propertyService.UpdatePropertyAsync(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut("Value")]
        public async Task<ActionResult<Updated>> UpdatePropertyValue([FromBody] PropertyValueUpdateDto updateDto)
        {
            var result = await propertyService.UpdatePropertyValueAsync(updateDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut("ValueList")]
        public async Task<ActionResult<Updated>> UpdatePropertyValueList([FromBody] PropertyValueUpdateDtoList updateDtoList)
        {
            var result = await propertyService.UpdatePropertyValuesListAsync(updateDtoList);

            return this.SendResponse(result);
        }
        #endregion


        #region Delete==============================================

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteTypeProperty(long id)
        {
            var result = await propertyService.DeletePropertyAsync(id);

            return this.SendResponse(result);
        }
        #endregion


        #region Get=================================================

        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<ProductTypeProperty>>> GetAllProperties()
        {
            var result = await propertyService.GetAllPropertiesAsync();

            return this.SendResponse(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<ProductTypeProperty>> GetPropertyById(long propertyId)
        {
            var result = await propertyService.GetPropertyByIdAsync(propertyId);

            return this.SendResponse(result);
        }

        [HttpGet("ProductType/{typeId}")]
        public async Task<ActionResult<List<ProductTypeProperty>>> GetPropertiesByProductTypeId(long typeId)
        {
            var result = await propertyService.GetPropertyByProductTypeIdAsync(typeId);

            return this.SendResponse(result);
        }


        //[Authorize(Roles = Roles.Administrator)]
        //[HttpGet]
        //public async Task<ActionResult<List<PropertyValue>>> GetAllPropertyValues()
        //{
        //    var result = await propertyService.GetAllPropertyValuesAsync();

        //    return this.SendResponse(result);
        //}


        [HttpGet("ProductType/{typeId}/UniqueValues")]
        public async Task<ActionResult<List<UniquePropertyValues>>> GetUniquePropertyValuesByProductTypeId(long typeId)
        {
            var result = await propertyService.GetUniquesPropertyValuesByProductTypeIdAsync(typeId);

            return this.SendResponse(result);
        }

        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<List<PropertyValue>>> GetPropertyValuesByProductId(long productId)
        {
            var result = await propertyService.GetPropertyValuesByProductIdAsync(productId);

            return this.SendResponse(result);
        }
        #endregion

    }
}
