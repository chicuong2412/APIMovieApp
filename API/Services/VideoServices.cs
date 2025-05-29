using API.DTOs;
using API.DTOs.Video;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using API.ReturnCodes.SuccessCodes;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class VideoServices : IVideoServices
    {

        private readonly IVideoRepository _videoRepository;
        public VideoServices(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<FileStream> GetVideoStream(string code)
        {
            var video = await _videoRepository.GetVideoByCodeAsync(code);

            var videoPath = Path.Combine("Videos", video.Url);
            var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read);
            return stream;
        }

        public (string filePath, string contentType) GetVideoHLS(string code, string fileName)
        {
            var videoPath = Path.Combine("D:\\Second\\videos\\" + code + "\\", fileName);
            if (File.Exists(videoPath) == false)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var contentType = fileName.EndsWith(".m3u8")
                        ? "application/vnd.apple.mpegurl"
                        : "video/MP2T";
            return (videoPath, contentType);
        }

        public async Task<APIresponse<Video>> GetVideoById(int id)
        {
            var video = await _videoRepository.GetVideoByIdAsync(id);
            
            var response = new APIresponse<Video>(SuccessCodes.Success);

            response.data = video;

            return response;
        }

        public async Task<APIresponse<IEnumerable<Video>>> GetAllVideos()
        {
            var videos = await _videoRepository.GetAllVideosAsync();

            var response = new APIresponse<IEnumerable<Video>>(SuccessCodes.Success);

            response.data = videos;

            return response;
        }

        public async Task<APIresponse<Video>> AddVideo(VideoCreationRequest request)
        {
            var video = await _videoRepository.AddVideoAsync(request);

            var response = new APIresponse<Video>(SuccessCodes.Success);

            response.data = video;

            return response;
        }

        public async Task<APIresponse<Video>> UpdateVideo(int id, VideoUpdationRequest request)
        {
            var video = await _videoRepository.UpdateVideoAsync(id, request);

            var response = new APIresponse<Video>(SuccessCodes.Success);

            response.data = video;

            return response;
        }

        public async Task<bool> DeleteVideo(int id)
        {
            var result = await _videoRepository.DeleteVideoAsync(id);
            return result;
        }

    }
}
