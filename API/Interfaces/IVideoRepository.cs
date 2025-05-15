using API.DTOs.Video;
using API.Models;

namespace API.Interfaces
{
    public interface IVideoRepository
    {
        Task<Video> GetVideoByCodeAsync(string code);
        Task<Video> GetVideoByIdAsync(int id);
        Task<IEnumerable<Video>> GetAllVideosAsync();
        Task<Video> AddVideoAsync(VideoCreationRequest request);
        Task<Video> UpdateVideoAsync(int id, VideoUpdationRequest request);
        Task<bool> DeleteVideoAsync(int id);
    }
}
