using Application.Models;
using Domain.Constants;
using Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController(IProductImageService service) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetImageById(long id)
        {
            var result = await service.GetImageById(id);

            return this.SendResponse(result);
        }

        [HttpGet("Product/{id}")]
        public async Task<ActionResult<List<ProductImage>>> GetImageByProductId(long id)
        {
            var result = await service.GetImagesByProductId(id);

            return this.SendResponse(result);
        }

        [HttpGet("Product/{id}/First")]
        public async Task<ActionResult<ProductImage>> GetFirstImageByProductId(long id)
        {
            var result = await service.GetFirstImageByProductId(id);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> UploadImage([FromBody] ProductImageAddDto addDto)
        {
            var result = await service.AddImage(addDto);

            return this.SendResponse(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deleted>> DeleteImageById(long id)
        {
            var result = await service.DeleteImage(id);

            return this.SendResponse(result);
        }

    }
}
