﻿using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TypePropertiesController(ITypePropertyService propertyService) : ControllerBase
    {

        [HttpGet("ProductType/{id}")]
        public async Task<IActionResult> GetPropertiesByTypeId(long id)
        {
            var result = await propertyService.GetPropertiesByTypeId(id);

            return result switch
            {
                SuccessResult<List<TypeProperty>> => Ok(result.Data),
                ErrorResult<List<TypeProperty>> errorResult => BadRequest(errorResult),
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
                ValidationErrorResult errorResult => BadRequest(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
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
                ValidationErrorResult errorResult => BadRequest(errorResult),
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
                NotFoundErrorResult errorResult => BadRequest(errorResult),
                ErrorResult errorResult => BadRequest(errorResult),
                _ => throw new ApplicationException()
            };
        }

    }
}
