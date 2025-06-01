using API.DTOs.Movies;
using API.DTOs;
using API.Interfaces;
using API.ReturnCodes.SuccessCodes;
using API.Mappers;
using API.Models;

namespace API.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;

        public MovieServices(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<APIresponse<MovieGeneralInformationReponse>> CreateMovie(MovieCreationRequest request)
        {
            var movie = await _movieRepository.CreateMovie(request);

            var reponse = new APIresponse<MovieGeneralInformationReponse>(SuccessCodes.Created);

            reponse.data = movie.MapToMovieGeneralResponse();

            return reponse;
        }

        public async Task DeleteMovie(int id)
        {
            await _movieRepository.DeleteMovie(id);
        }

        public async Task<APIresponse<MovieDetailInformation>> GetMovieDetailInformationById(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            var response = new APIresponse<MovieDetailInformation>(SuccessCodes.Success);
            response.data = movie.MoveToDetail();
            return response;
        }

        public async Task<APIresponse<IEnumerable<MovieGeneralInformationReponse>>> GetMoviesAsync(ObjectFilter filter)
        {
            var movies = await _movieRepository.GetMoviesAsync(filter);
            var response = new APIresponse<IEnumerable<MovieGeneralInformationReponse>>(SuccessCodes.Success);
            var movieList = movies.Select(m => m.MapToMovieGeneralResponse());
            response.data = movieList;
            return response;
        }

        public async Task<APIresponse<MovieGeneralInformationReponse>> UpdateMovie(int id, MovieUpdationRequest request)
        {
            var movie = await _movieRepository.UpdateMovie(id, request);
            var response = new APIresponse<MovieGeneralInformationReponse>(SuccessCodes.Success);
            response.data = movie.MapToMovieGeneralResponse();

            return response;
        }

        public async Task<APIresponse<List<MovieGeneralInformationReponse>>> GetTop20ReleasedMovies()
        {
            var movies = await _movieRepository.GetMoviesAsync(new ObjectFilter()
            {
                Page = 1,
                PageSize = 20,
                SortBy = "ReleaseDate",
                IsAscending = false,
            });
            var response = new APIresponse<List<MovieGeneralInformationReponse>>(SuccessCodes.Success);
            var movieList = movies.Select(m => m.MapToMovieGeneralResponse()).ToList();
            response.data = movieList;
            return response;
        }

        public async Task<APIresponse<IEnumerable<MovieGeneralInformationReponse>>> Get9RandomMovies()
        {
            var movies = await _movieRepository.GetRadMoviesAsync(9);
            var response = new APIresponse<IEnumerable<MovieGeneralInformationReponse>>(SuccessCodes.Success);
            var movieList = movies.Select(m => m.MapToMovieGeneralResponse()).ToList();
            response.data = movieList;
            return response;
        }
    }
}
