using API.DTOs.Video;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IVideoServices
    {
        Task<FileStream> GetVideoStream(string code);
        (string filePath, string contentType) GetVideoHLS(string code, string fileName);
        Task<APIresponse<Video>> UpdateVideo(int id, VideoUpdationRequest request);
        Task<APIresponse<Video>> AddVideo(VideoCreationRequest request);
        Task<APIresponse<IEnumerable<Video>>> GetAllVideos();
        Task<APIresponse<Video>> GetVideoById(int id);
        Task<bool> DeleteVideo(int id);

    }
}
