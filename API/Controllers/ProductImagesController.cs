using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController(IProductImageService service) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(long id)
        {
            var result = await service.GetImageById(id);

            return result switch
            {
                SuccessResult<ProductImage> => Ok(result),
                NotFoundErrorResult<ProductImage> => NotFound(result),
                ErrorResult<ProductImage> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetImageByProductId(long id)
        {
            var result = await service.GetImagesByProductId(id);

            return result switch
            {
                SuccessResult<List<ProductImage>> => Ok(result),
                NotFoundErrorResult<List<ProductImage>> => NotFound(result),
                ErrorResult<List<ProductImage>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpGet("Product/{id}/First")]
        public async Task<IActionResult> GetFirstImageByProductId(long id)
        {
            var result = await service.GetFirstImageByProductId(id);

            return result switch
            {
                SuccessResult<ProductImage> => Ok(result),
                NotFoundErrorResult<ProductImage> => NotFound(result),
                ErrorResult<ProductImage> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] ProductImageAddDto addDto)
        {
            var result = await service.AddImage(addDto);

            return result switch
            {
                SuccessResult => Created(),
                ValidationErrorResult => StatusCode(422, result),
                NotFoundErrorResult => NotFound(result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageById(long id)
        {
            var result = await service.DeleteImage(id);

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
