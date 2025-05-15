using API.DTOs.Authentication;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationServices _authenticationServices;
        public AuthenticationController(AuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authenticationServices.RegisterUser(request);
            if (response == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(response);
        }

    }
}
