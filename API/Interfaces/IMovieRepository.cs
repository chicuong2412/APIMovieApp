using API.DTOs;
using API.DTOs.Movies;
using API.Models;

namespace API.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(ObjectFilter filter);
        Task<Movie> GetMovieById(int id);
        Task<Movie> CreateMovie(MovieCreationRequest request);
        Task<Movie> UpdateMovie(int id, MovieUpdationRequest request);
        Task DeleteMovie(int id);

        Task<IEnumerable<Movie>> GetRadMoviesAsync(int count);
    }
}
