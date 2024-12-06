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
    public class ProductImagesController(IProductImageService prodImageService) : ControllerBase
    {

        [HttpGet("{imageId}")]
        public async Task<ActionResult<ProductImage>> GetImageById(long imageId)
        {
            var result = await prodImageService.GetByIdAsync(imageId);

            return this.SendResponse(result);
        }


        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<List<ProductImage>>> GetImagesByProductId(long productId)
        {
            var result = await prodImageService.GetAllByProductIdAsync(productId);

            return this.SendResponse(result);
        }


        [HttpGet("Product/{productId}/First")]
        public async Task<ActionResult<ProductImage>> GetFirstImageByProductId(long productId)
        {
            var result = await prodImageService.GetFirstByProductIdAsync(productId);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<Created>> UploadImage([FromBody] ProductImageAddDto addDto)
        {
            var result = await prodImageService.AddAsync(addDto);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{imageId}")]
        public async Task<ActionResult<Deleted>> DeleteImageById(long imageId)
        {
            var result = await prodImageService.DeleteByIdAsync(imageId);

            return this.SendResponse(result);
        }

    }
}
