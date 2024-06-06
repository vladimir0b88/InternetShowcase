using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
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

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetPropertyById(long propertyId)
        {
            var result = await propertyService.GetPropertyById(propertyId);

            return result switch
            {
                SuccessResult<TypeProperty> => Ok(result),
                NotFoundErrorResult<TypeProperty> => NotFound(result),
                ErrorResult<TypeProperty> => BadRequest(result),
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

        [Authorize(Roles = Roles.Administrator)]
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

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> UpdateTypeProperty([FromBody]TypePropertyUpdateDto updateDto)
        {
            var result = await propertyService.UpdateProperty(updateDto);

            return result switch
            {
                SuccessResult => Ok(result),
                ValidationErrorResult => StatusCode(422, result),
                ErrorResult  => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProperty(long id)
        {
            var result = await propertyService.DeleteProperty(id);

            return result switch
            {
                SuccessResult => Ok(result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

    }
}
