using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services;
using API.DTOs;
using API.DTOs.Episode;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeServices _episodeServices;

        public EpisodesController(IEpisodeServices episodeServices)
        {
            _episodeServices = episodeServices;
        }

        // GET: api/Episodes
        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("season/{id:int}")]
        public async Task<ActionResult<APIresponse<IEnumerable<EpisodeGeneralReponse>>>> GetEpisodesBySeasonId(int id, [FromQuery] ObjectFilter objectFilter)
        {
            return Ok(await _episodeServices.GetEpisodesBySeasonId(id, objectFilter));
        }

        // GET: api/Episodes/5
        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIresponse<EpisodeGeneralReponse>>> GetEpisode(int id)
        {
            return Ok(await _episodeServices.GetEpisodeById(id));
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(policy: "Update")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIresponse<EpisodeGeneralReponse>>> PutEpisode(int id, EpisodeUpdationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _episodeServices.UpdateEpisode(id, request);

            return Ok(response);
        }

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(policy: "Create")]
        [HttpPost]
        public async Task<ActionResult<APIresponse<EpisodeGeneralReponse>>> PostEpisode(EpisodeCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _episodeServices.CreateEpisode(request);

            return Ok(response);
        }

        //PUT: api/Episodes/5/season/4
        [Authorize(policy: "Update")]
        [HttpPut("{idEpisode:int}/season/{seasonId:int}")]
        public async Task<ActionResult<APIresponse<EpisodeGeneralReponse>>> AddEpisodeToSeason(int idEpisode, int seasonId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _episodeServices.AddEpisodeToSeason(seasonId, idEpisode);
            return Ok(response);
        }

        // PUT: api/Episodes/5/season/4
        [Authorize(policy: "Update")]
        [HttpPut("{idEpisode:int}/season/{seasonId:int}/remove")]
        public async Task<ActionResult<APIresponse<string>>> RemoveEpisodeFromSeason(int idEpisode, int seasonId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _episodeServices.RemoveEpisodeFromSeason(seasonId, idEpisode);
            return Ok(response);
        }

        // DELETE: api/Episodes/5
        [Authorize(policy: "Delete")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIresponse<string>>> DeleteEpisode(int id)
        {
            return Ok(await _episodeServices.DeleteEpisode(id));
        }

    }
}
