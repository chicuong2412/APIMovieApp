using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.DTOs.Video;
using API.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/videos")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IVideoServices _videoServices;

        public VideosController(IVideoServices videoServices)
        {
            _videoServices = videoServices;
        }

        // GET: api/Videos/5
        //[Authorize(policy: "View")]
        [AllowAnonymous]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetVideo(string code)
        {
            var reponse = await _videoServices.GetVideoStream(code);

            Console.WriteLine("Done stream");

            return Ok(reponse);
        }

        [AllowAnonymous]
        [HttpGet("{code}/{fileName}")]
        public async Task<IActionResult> GetVideoHLS([FromRoute] String code, [FromRoute] String fileName)
        {
            var (filePath, contentType) = _videoServices.GetVideoHLS(code, fileName);

            Console.WriteLine(fileName);

            return PhysicalFile(filePath, contentType, enableRangeProcessing: true);
        }

        [Authorize(policy: "Update")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIresponse<Video>>> PutVideo(int id, VideoUpdationRequest request)
        {
            var reponse = await _videoServices.UpdateVideo(id, request);

            return Ok(reponse);
        }

        // POST: api/Videos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(policy: "Create")]
        [HttpPost]
        public async Task<ActionResult<APIresponse<Video>>> PostVideo([FromBody] VideoCreationRequest request)
        {
            var response = await _videoServices.AddVideo(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        // DELETE: api/Videos/5
        [Authorize(policy: "Delete")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var response = await _videoServices.DeleteVideo(id);

            return Ok(response);
        }
    }
}
