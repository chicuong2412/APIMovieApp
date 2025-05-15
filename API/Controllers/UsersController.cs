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
        public async Task<ActionResult<UserGlobalReponse>> UpdateUser(string id, UserUpdationRequest request)
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

    }
}
