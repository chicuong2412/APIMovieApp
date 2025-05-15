using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {


        [HttpGet("{path}")]
        public IActionResult Get(string path)
        {
            var imagePath = Path.Combine("Images", "D:\\Second\\images\\" + path);
            if (!System.IO.File.Exists(imagePath))
            {
                imagePath = Path.Combine("Images", "D:\\Second\\images\\default.jpg");
            }
            var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            var response = File(stream, "image/jpeg", enableRangeProcessing: true);
            return response;
        }

    }
}
