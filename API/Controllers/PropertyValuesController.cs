using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.PropertyValues.Update;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyValuesController(IPropertyValueService propertyValueService) : ControllerBase
    {

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetPropertyValuesByProductId(long id)
        {
            var result = await propertyValueService.GetPropertyValuesByProductId(id);

            return result switch
            {
                SuccessResult<List<PropertyValue>> => Ok(result.Data),
                ErrorResult<List<PropertyValue>> errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePropertyValue([FromBody]PropertyValueUpdateDto updateDto)
        {
            var result = await propertyValueService.UpdatePropertyValue(updateDto);

            return result switch
            {
                SuccessResult => Ok(),
                ValidationErrorResult errorResult => BadRequest(errorResult),
                NotFoundErrorResult errorResult => NotFound(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }
    }
}
