using API.DTOs;
using API.DTOs.Movies;
using API.Models;

namespace API.Interfaces
{
    public interface IMovieServices
    {
        Task<APIresponse<MovieGeneralInformationReponse>> CreateMovie(MovieCreationRequest request);
        Task<APIresponse<MovieGeneralInformationReponse>> UpdateMovie(int id, MovieUpdationRequest request);
        Task DeleteMovie(int id);
        Task<APIresponse<IEnumerable<MovieGeneralInformationReponse>>> GetMoviesAsync(ObjectFilter filter);
        Task<APIresponse<MovieGeneralInformationReponse>> GetMovieGeneralInformationById(int id);
        Task<APIresponse<List<MovieGeneralInformationReponse>>> GetTop20ReleasedMovies();

        Task<APIresponse<MovieGeneralInformationReponse>> Get9RandomMovies();

    }
}
