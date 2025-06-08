using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Interfaces;
using API.DTOs;
using API.DTOs.Season;
using API.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/seasons")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonServices _seasonServices;

        public SeasonsController(ISeasonServices seasonServices)
        {
            _seasonServices = seasonServices;
        }

        // GET: api/Seasons
        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("getSeasonsByMoiveId/{id:int}")]
        public async Task<ActionResult<APIresponse<List<SeasonGeneralInformation>>>> GetSeasonsByMoiveId(int id)
        {
            return Ok(await _seasonServices.GetSeasonsByMovieId(id));
        }

        // GET: api/Seasons/5
        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIresponse<SeasonGeneralInformation>>> GetSeason(int id)
        {
            return Ok(await _seasonServices.GetSeasonGeneralInformationById(id));
        }

        // PUT: api/Seasons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(policy: "Update")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIresponse<SeasonGeneralInformation>>> PutSeason(int id, SeasonUpdationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _seasonServices.UpdateSeason(id, request));
        }

        // POST: api/Seasons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(policy: "Create")]
        [HttpPost]
        public async Task<ActionResult<APIresponse<SeasonGeneralInformation>>> PostSeason(SeasonCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _seasonServices.CreateSeason(request));
        }

        [Authorize(policy: "Update")]
        [HttpPut("add-to-movie/{movieId:int}/{idSeason:int}")]
        public async Task<ActionResult<APIresponse<string>>> AddSeasonToMovie(int movieId, int idSeason)
        {
            return Ok(await _seasonServices.AddSeasonToMovie(movieId, idSeason));
        }

        [Authorize(policy: "Update")]
        [HttpPut("remove-from-movie/{movieId:int}/{idSeason:int}")]
        public async Task<ActionResult<APIresponse<string>>> RemoveSeasonFromMovie(int movieId, int idSeason)
        {
            return Ok(await _seasonServices.RemoveSeasonFromMovie(movieId, idSeason));
        }

        // DELETE: api/Seasons/5
        [Authorize(policy: "Delete")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSeason(int id)
        {
            await _seasonServices.DeleteSeason(id);

            return NoContent();
        }

    }
}
