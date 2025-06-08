using API.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/generes")]
    [ApiController]
    public class GenereController : ControllerBase
    {

        private readonly GenereServices _genereServices;

        public GenereController(GenereServices genereServices)
        {
            _genereServices = genereServices;
        }


        [HttpGet("getAll")]
        public async Task<ActionResult<APIresponse<List<Genere>>>> GetAllGeneres()
        {
            return Ok(await _genereServices.GetAllGeneres());
        }
        

    }
}
