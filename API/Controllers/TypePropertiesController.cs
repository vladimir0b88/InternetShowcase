using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TypePropertiesController(ITypePropertyService propertyService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTypeProperties()
        {
            var result = await propertyService.GetAllTypeProperties();

            return result switch
            {
                SuccessResult<List<TypeProperty>> => Ok(result),
                ErrorResult<List<TypeProperty>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpGet("ProductType/{id}")]
        public async Task<IActionResult> GetPropertiesByProductTypeId(long id)
        {
            var result = await propertyService.GetPropertiesByProductTypeId(id);

            return result switch
            {
                SuccessResult<List<TypeProperty>> => Ok(result),
                ErrorResult<List<TypeProperty>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }


        [HttpPost]
        public async Task<IActionResult> AddTypeProperty([FromBody]TypePropertyCreateDto createDto)
        {
            var result = await propertyService.AddProperty(createDto);

            return result switch
            {
                SuccessResult => Created(),
                ValidationErrorResult => StatusCode(422, result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTypeProperty([FromBody]TypePropertyUpdateDto updateDto)
        {
            var result = await propertyService.UpdateProperty(updateDto);

            return result switch
            {
                SuccessResult => Ok(),
                ValidationErrorResult => StatusCode(422, result),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProperty(long id)
        {
            var result = await propertyService.DeleteProperty(id);

            return result switch
            {
                SuccessResult => Ok(),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

    }
}
