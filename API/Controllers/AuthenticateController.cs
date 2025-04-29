using API.DTOs.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        [HttpPost("login")]
        public void Login([FromBody] AuthenticateLoginRequest request)
        {

        }
    }
}
