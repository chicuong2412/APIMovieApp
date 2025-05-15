using API.Data;
using API.DTOs;
using API.DTOs.Movies;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Mappers;
using API.Models;
using API.ReturnCodes.SuccessCodes;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> CreateMovie(MovieCreationRequest request)
        {
            var movie = MovieMapper.MapToMovie(request);

            if (!movie.HasSeason)
            {
                movie.path = request.Path;
            }

            try
            {
                await _context.Movies.AddAsync(movie);
            }
            catch (Exception)
            {
                throw new AppException(ErrorCodes.ServerError);
            }

            await SaveChangesAsysc();

            return movie;
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            movie.IsDeleted = true;

            await SaveChangesAsysc();
        }
        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _context.Movies
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (movie == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return movie;
        }
        public async Task<IEnumerable<Movie>> GetMoviesAsync(ObjectFilter filter)
        {
            var query = _context.Movies
                .Where(x => x.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(x => x.Title.Contains(filter.Search));
            }

            if (filter.PageSize > 0)
            {
                query = query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);
            }

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                if (filter.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.IsAscending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                }
                else if (filter.SortBy.Equals("ReleaseDate", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.IsAscending ? query.OrderBy(x => x.ReleaseDate) : query.OrderByDescending(x => x.ReleaseDate);
                }
                else if (filter.SortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.IsAscending ? query.OrderBy(x => x.VoteCount) : query.OrderByDescending(x => x.VoteCount);
                }
                else if (filter.SortBy.Equals("Revenue", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.IsAscending ? query.OrderBy(x => x.Revenue) : query.OrderByDescending(x => x.Revenue);
                }
                else if (filter.SortBy.Equals("Budget", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.IsAscending ? query.OrderBy(x => x.Budget) : query.OrderByDescending(x => x.Budget);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetRadMoviesAsync(int count)
        {
            var query = _context.Movies.Where(m => !m.IsDeleted).AsQueryable();

            return await query.OrderBy(m => Guid.NewGuid()).Take(count).ToListAsync();
        }

        public async Task<Movie> UpdateMovie(int id, MovieUpdationRequest request)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            movie = movie.MovieUpdationRequestToMovie(request);

            if (movie.HasSeason)
            {
                if (!string.IsNullOrWhiteSpace(request.Path))
                {
                    movie.path = request.Path;
                }
            }

            _context.Entry(movie).State = EntityState.Modified;

            await SaveChangesAsysc();

            return movie;
        }

        private async Task SaveChangesAsysc()
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
