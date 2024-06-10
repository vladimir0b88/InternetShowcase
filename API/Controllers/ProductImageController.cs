using Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        [HttpPost("{productId:long}")]
        public async Task<IActionResult> UploadImage()
        {




            return Ok(new SuccessResult());
        }

    }
}
