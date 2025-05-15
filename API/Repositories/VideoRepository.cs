using API.Data;
using API.DTOs.Video;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class VideoRepository : IVideoRepository
    {

        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Video> AddVideoAsync(VideoCreationRequest request)
        {
            var video = VideoMappers.MapToVideo(request);

            await _context.Videos.AddAsync(video);


            await SaveAllAsync();

            return video;
        }

        public async Task<bool> DeleteVideoAsync(int id)
        {
            var video = _context.Videos.FirstOrDefault(v => v.Id == id);

            if (video == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            video.IsDeleted = true;

            _context.Videos.Update(video);
            await SaveAllAsync();

            return true;
        }

        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _context.Videos
                .Where(v => v.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Video> GetVideoByCodeAsync(string code)
        {
            var video = await _context.Videos
                .FirstOrDefaultAsync(v => v.Code == code && v.IsDeleted == false);

            if (video == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            return video;
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            var video = await _context.Videos
                .FirstOrDefaultAsync(v => v.Id == id && v.IsDeleted == false);

            if (video == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return video;
        }

        public async Task<Video> UpdateVideoAsync(int id, VideoUpdationRequest request)
        {
            var existingVideo = await _context.Videos
                .FirstOrDefaultAsync(v => v.Id == id && v.IsDeleted == false);
            if (existingVideo == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            existingVideo = VideoMappers.MapToVideo(request);

            _context.Videos.Entry(existingVideo).State = EntityState.Modified;

            await SaveAllAsync();

            return existingVideo;
        }

        private async Task SaveAllAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException(ErrorCodes.ServerError);
            }
        }
    }
}
