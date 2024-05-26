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
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyValues()
        {
            var result = await propertyValueService.GetAllPropertyValues();

            return result switch
            {
                SuccessResult<List<PropertyValue>> => Ok(result),
                ErrorResult<List<PropertyValue>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }


        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetPropertyValuesByProductId(long id)
        {
            var result = await propertyValueService.GetPropertyValuesByProductId(id);

            return result switch
            {
                SuccessResult<List<PropertyValue>> => Ok(result),
                ErrorResult<List<PropertyValue>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePropertyValue([FromBody]PropertyValueUpdateDto updateDto)
        {
            var result = await propertyValueService.UpdatePropertyValue(updateDto);

            return result switch
            {
                SuccessResult => Ok(result),
                ValidationErrorResult => StatusCode(422, result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPut("List")]
        public async Task<IActionResult> UpdatePropertyValueList([FromBody] PropertyValueUpdateDtoList updateDtoList)
        {
            var result = await propertyValueService.UpdatePropertyValueList(updateDtoList);

            return result switch
            {
                SuccessResult => Ok(result),
                ValidationErrorResult => StatusCode(422, result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

    }
}
