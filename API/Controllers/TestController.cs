using API.ENUMS.ErrorCodes;
using API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            throw new AppException(ErrorCodes.Expired);
        }
    }
}
