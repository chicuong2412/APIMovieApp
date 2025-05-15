using API.Data;
using API.DTOs.Season;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SeasonRepository : Interfaces.ISeasonRepository
    {
        private readonly AppDbContext _context;
        public SeasonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Season> GetSeasonById(int id)
        {
            var season = await _context.Seasons
                .Include(s => s.Episodes)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);
            Console.WriteLine("Episodes:");
            Console.WriteLine(season?.Episodes.Count());
            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return season;
        }
        public async Task<Season> CreateSeason(SeasonCreationRequest request)
        {
            var season = SeasonMapper.MapToSeason(request);

            await _context.Seasons.AddAsync(season);

            await SaveChangesAsync();

            return season;
        }

        public async Task<Season> GetSeasonById(int id, bool includeEpisodes)
        {
            if (includeEpisodes)
            {
                var season = await _context.Seasons
                    .Include(s => s.Episodes)
                    .FirstOrDefaultAsync(s => s.Id == id);
                if (season == null)
                {
                    throw new AppException(ErrorCodes.NotFound);
                }
                return season;
            }
            else
            {
                return await GetSeasonById(id);
            }
        }

        public async Task AddSeasonToMovie(int movieId, int idSeason)
        {
            var movie = await _context.Movies
                .Include(m => m.Seasons)
                .FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var season = await _context.Seasons.FindAsync(idSeason);
            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            if (season.MovieId != null)
            {
                throw new AppException(ErrorCodes.DataConflict);
            }

            movie.Seasons.Add(season);
            await SaveChangesAsync();
        }

        public async Task RemoveSeasonFromMovie(int movieId, int idSeason)
        {
            var movie = await _context.Movies
                .Include(m => m.Seasons)
                .FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var season = await _context.Seasons.FindAsync(idSeason);
            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            if (season.MovieId != movieId)
            {
                throw new AppException(ErrorCodes.DataConflict);
            }

            movie.Seasons.Remove(season);
            await SaveChangesAsync();
        }

        public async Task<Season> UpdateSeason(int id, SeasonUpdationRequest request)
        {
            var season = await _context.Seasons.FindAsync(id);

            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            season = season.MapToSeason(request);

            _context.Entry(season).State = EntityState.Modified;

            await SaveChangesAsync();

            return season;
        }
        public async Task<List<Season>> GetSeasonsByMovieId(int movieId)
        {
            var seasons = await _context.Seasons
                .Where(s => s.MovieId == movieId)
                .Where(s => s.IsDeleted == false)
                .ToListAsync();

            if (seasons == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return seasons;
        }
        public async Task<List<Season>> GetAllSeasonsWithEpisodesByMovieId(int movieId)
        {
            var seasons = await _context.Seasons
                .Include(s => s.Episodes)
                .Where(s => s.MovieId == movieId)
                .Where(s => s.IsDeleted == false)
                .ToListAsync();
            if (seasons == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return seasons;
        }
        public async Task DeleteSeason(int id)
        {
            var season = await _context.Seasons.FindAsync(id);
            if (season == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            
            season.IsDeleted = true;

            _context.Entry(season).State = EntityState.Modified;

            await SaveChangesAsync();
        }

        private Task SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException(ErrorCodes.ServerError);
            }
        }
    }
}
