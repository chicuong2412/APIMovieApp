using API.DTOs;
using API.DTOs.Authentication;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly AuthenticationServices _authenticationServices;

        public AuthenticateController(AuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticateLoginRequest request)
        {
            var repsponse = await _authenticationServices.ValidateUser(request.Username, request.Password);

            if (repsponse is null)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(repsponse);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<APIresponse<string>>> Register([FromBody] RegisterRequest request)
        {
            var response = await _authenticationServices.RegisterUser(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string requestToken)
        {
            if (string.IsNullOrEmpty(requestToken))
            {
                return BadRequest("Invalid token");
            }
            var jwt = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            await _authenticationServices.Logout(requestToken, jwt);
            return NoContent();
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticationResponse>> RefreshToken([FromBody] string requestToken)
        {
            var response = await _authenticationServices.ValidateRefreshToken(requestToken);
            if (response is null)
            {
                return BadRequest("Invalid refresh token");
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("forgot-password/{email}")]
        public async Task<ActionResult<APIresponse<string>>> GetResetCode(string email)
        {
            var reponse = await _authenticationServices.ForgotPasswordGetCode(email);

            return Ok(reponse);
        }

        [AllowAnonymous]
        [HttpPost("validate-code/{email}")]
        public async Task<ActionResult<APIresponse<string>>> ValidateCode(string email, [FromBody] string code)
        {
            return await _authenticationServices.ValidateResetCode(email, code);
        }

        [AllowAnonymous]
        [HttpPut("change-password/{email}")]
        public async Task<ActionResult<APIresponse<string>>> ChangePassword(string email, [FromBody] string newPassword)
        {
            return await _authenticationServices.ChangePassword(email, newPassword);
        }



    }
}
