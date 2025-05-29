using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest refreshToken)
        {
            Console.WriteLine("Logout");
            if (string.IsNullOrEmpty(refreshToken.RequestToken))
            {
                return BadRequest("Invalid token");
            }
            var jwt = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            await _authenticationServices.Logout(refreshToken.RequestToken, jwt);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticationResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshToken)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest("Input the refresh token, please");
            }

            var response = await _authenticationServices.ValidateRefreshToken(refreshToken.RequestToken);
            if (response is null)
            {
                Console.WriteLine("Null");
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
        [HttpPost("validate-code/{token}")]
        public async Task<ActionResult<APIresponse<string>>> ValidateCode(string token, [FromBody] CodeForgot code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _authenticationServices.ValidateResetCode(token, code.Code));
        }

        [AllowAnonymous]
        [HttpPut("change-password")]
        public async Task<ActionResult<APIresponse<string>>> ChangePassword(ChangePassRequest changePassRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _authenticationServices.ChangePassword(changePassRequest.NewPassword, changePassRequest.Token));
        }



    }
}
