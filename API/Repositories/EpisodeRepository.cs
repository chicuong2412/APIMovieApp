using API.Data;
using API.DTOs;
using API.DTOs.Episode;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;
        public EpisodeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            var episode = await _context.Episodes
                .FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);
            if (episode == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return episode;
        }
        public async Task<IEnumerable<Episode>> GetAllEpisodesAsync()
        {
            return await _context.Episodes.ToListAsync();
        }
        public async Task<Episode> AddEpisodeAsync(EpisodeCreationRequest request)
        {
            var episode = EpisodeMapper.CreationToEpisode(request);

            _context.Episodes.Add(episode);

            await _context.SaveChangesAsync();

            try
            {
                if (request.SeasonId.HasValue)
                {
                    await AddEpisodeToSeason(request.SeasonId.Value, episode.Id);
                }              
            }
            catch (Exception)
            {
                
            }

            return episode;
        }
        public async Task<Episode> UpdateEpisodeAsync(int id, EpisodeUpdationRequest request)
        {
            var episode =await _context.Episodes.FindAsync(id);

            if (episode == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            episode = EpisodeMapper.UpdationToEpisode(episode, request);

            _context.Entry(episode).State = EntityState.Modified;

            await SaveChangesAsync();

            try
            {
                if (request.SeasonId.HasValue)
                {
                    await AddEpisodeToSeason(request.SeasonId.Value, episode.Id);
                }
            }
            catch (Exception)
            {

            }

            return episode;
        }
        public async Task DeleteEpisodeAsync(int id)
        {
            var episode = await GetEpisodeByIdAsync(id);
            if (episode != null)
            {
                episode.IsDeleted = true;
                _context.Episodes.Update(episode);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Episode>> GetEpisodesBySeasonIdAsync(int seasonId, ObjectFilter objectFilter)
        {

            var episodes = await _context.Episodes
                .Where(e => e.SeasonId == seasonId && e.IsDeleted == false)
                //.Skip((objectFilter.Page - 1) * objectFilter.PageSize)
                //.Take(objectFilter.PageSize)
                .ToListAsync();

            if (episodes == null)
            {
                throw new AppException(ErrorCodes.NoSeasons);
            }
            return episodes;
        }

        public async Task AddEpisodeToSeason(int seasonId, int episodeId)
        {
            var season = await _context.Seasons
                .Include(s => s.Episodes)
                .FirstOrDefaultAsync(s => s.Id == seasonId && s.IsDeleted == false);
            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var episode = await _context.Episodes
                .FirstOrDefaultAsync(e => e.Id == episodeId && e.IsDeleted == false);
            if (episode == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            if (episode.SeasonId != null)
            {
                throw new AppException(ErrorCodes.Conflict);
            }

            season.Episodes.Add(episode);
            await SaveChangesAsync();
        }

        public async Task RemoveEpisodeFromSeason(int seasonId, int episodeId)
        {
            var season = await _context.Seasons
                .Include(s => s.Episodes)
                .FirstOrDefaultAsync(s => s.Id == seasonId && s.IsDeleted == false);
            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var episode = await _context.Episodes
                .FirstOrDefaultAsync(e => e.Id == episodeId && e.IsDeleted == false);
            if (episode == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            if (episode.SeasonId != season.Id)
            {
                throw new AppException(ErrorCodes.Conflict);
            }
            season.Episodes.Remove(episode);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
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
