﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Interfaces;
using API.Repositories;
using API.DTOs;
using API.DTOs.Movies;
using API.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices _moveServices;

        public MoviesController(IMovieServices movieServices)
        {
            _moveServices = movieServices;
        }

        // GET: api/Movies/search
        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("search")]
        public async Task<ActionResult<APIresponse<IEnumerable<MovieGeneralInformationReponse>>>> GetMovies([FromQuery] ObjectFilter objectFilter)
        {
            return Ok(await _moveServices.GetMoviesAsync(objectFilter));
        }

        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("getRandom")]
        public async Task<ActionResult<APIresponse<IEnumerable<MovieGeneralInformationReponse>>>> GetRandomMovies()
        {
            return Ok(await _moveServices.Get9RandomMovies());
        }

        // GET: api/Movies/5
        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIresponse<MovieGeneralInformationReponse>>> GetMovie(int id)
        {
            return Ok(await _moveServices.GetMovieGeneralInformationById(id));
        }



        [Authorize(policy: "Update")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIresponse<MovieGeneralInformationReponse>>> PutMovie(int id, MovieUpdationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _moveServices.UpdateMovie(id, request);
            return Ok(response);
        }


        [Authorize(policy: "Create")]
        [HttpPost]
        public async Task<ActionResult<APIresponse<MovieGeneralInformationReponse>>> PostMovie(MovieCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _moveServices.CreateMovie(request);

            return response;
        }


        [Authorize(policy: "CAN_GET_INFO")]
        [HttpGet("discover")]
        public async Task<ActionResult<APIresponse<IEnumerable<Movie>>>> DiscoverMovies()
        {
            return Ok(await _moveServices.GetTop20ReleasedMovies());
        }

        // DELETE: api/Movies/5
        [Authorize(policy: "Delete")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _moveServices.DeleteMovie(id);

            return NoContent();
        }

    }
}
