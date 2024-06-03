using BlogCentral.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Net;

namespace BlogCentral.Web.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await this._imageRepository.UploadAsync(file);

            if(imageUrl == null)
            {
                //return UnprocessableEntity();
                return Problem("Something is wrong, failed to upload image to our cloud provider.", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { imageUrl });
        }
    }
}
