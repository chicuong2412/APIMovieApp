using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.DTOs.Users;
using API.Mappers;
using API.Services;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NuGet.Versioning;
using API.DTOs.Movies;
using API.ReturnCodes.SuccessCodes;
using API.ENUMS.ErrorCodes;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userServices;

        public UsersController(IUserService userServices)
        {
            _userServices = userServices;
        }

        // GET: api/Users
        [Authorize(policy: "Get_Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGlobalReponse>>> GetUsers()
        {
            return Ok(await _userServices.GetAllUsers());
        }

        // GET: api/Users/5
        [Authorize(policy: "Get_Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGlobalReponse>> GetUser(string id)
        {
            return Ok(await _userServices.GetUserById(id));
        }

        // PUT: api/Users/5
        [Authorize(policy: "Update")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserGlobalReponse>> UpdateUser(string id, [FromForm] UserUpdationRequest request)
        {
            return Ok(await _userServices.UpdateUser(id, request));
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(policy: "Create")]
        [HttpPost]
        public async Task<ActionResult<APIresponse<UserGlobalReponse>>> CreateUser([FromBody] UserCreationRequest request)
        {
            return Ok(await _userServices.CreateUser(request));
        }

        // DELETE: api/Users/5
        [Authorize(policy: "Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIresponse<string>>> DeleteUser(string id)
        {
            return Ok(await _userServices.DeleteById(id));
        }

        [Authorize(policy: "CAN_GET_INFO")]
        [HttpPost("add-favorite-movie/{movieId:int}")]
        public async Task AddMovieFavorite(int movieId)
        {
            var identity = User.Claims;

            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await _userServices.AddMovieFavorite(userId!, movieId);
        }

        [Authorize(policy: "CAN_GET_INFO")]
        [HttpPost("remove-favorite-movie/{movieId:int}")]
        public async Task RemoveFavoriteMovie(int movieId)
        {
            var identity = User.Claims;

            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await _userServices.RemoveMovieFavorite(userId!, movieId);
        }

        [Authorize(policy: "CAN_GET_INFO")]
        [HttpPost("checkFavorite/{movieId:int}")]
        public async Task<ActionResult<APIresponse<bool>>> CheckFavoriteMovie(int movieId)
        {
            var identity = User.Claims;

            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok(await _userServices.IsMovieFavorite(userId!, movieId));
        }

        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("get-favorite-movies")]
        public async Task<ActionResult<APIresponse<List<MovieGeneralInformationReponse>>>> GetFavoriteMovies()
        {

            var identity = User.Claims;

            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok(await _userServices.GetFavoriteMovies(userId!));
        }


        [Authorize(policy: "CAN_GET_INFO")]
        [HttpPost("update-screen-time")]
        public async Task<ActionResult> UpdateUserScreenTime([FromBody] ScreenTime screenTime)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new APIresponse<string>(ErrorCodes.DataInvalid) { data = "Invalid model state" });
            }

            Console.WriteLine("Screen Time added: " + screenTime.Value);

            var identity = User.Claims;
            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _userServices.UpdateUserScreenTime(userId!, screenTime.Value);
            return Ok(new APIresponse<string>(SuccessCodes.Success) { data = "Screen time updated successfully" });
        }


        [Authorize(policy: "CAN_GET_INFO")]
        [HttpPut("update-my-profile")]
        public async Task<ActionResult<APIresponse<string>>> UpdateMyProfile([FromForm] UserUpdationRequest request)
        {
            var identity = User.Claims;
            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new APIresponse<string>(ErrorCodes.DataInvalid) { data = "User ID is missing" });
            }
            return Ok(await _userServices.UpdateUser(userId, request));
        }


        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("get-my-profile")]
        public async Task<ActionResult<APIresponse<UserGlobalReponse>>> GetMyProfile()
        {
            var identity = User.Claims;
            if (identity == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            var userId = identity.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new APIresponse<string>(ErrorCodes.DataInvalid) { data = "User ID is missing" });
            }

            return Ok(await _userServices.GetUserById(userId));
        }
    }
}
