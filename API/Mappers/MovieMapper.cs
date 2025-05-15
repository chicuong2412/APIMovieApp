using API.DTOs.Movies;
using API.Models;

namespace API.Mappers
{
    public static class MovieMapper
    {
        public static Movie MapToMovie(MovieCreationRequest request)
        {
            return new Movie
            {
                Title = request.Title,
                Overview = request.Overview,
                ReleaseDate = request.ReleaseDate,
                BackdropPath = request.BackdropPath,
                PosterPath = request.PosterPath,
                Revenue = request.Revenue,
                Budget = request.Budget,
                VoteCount = request.VoteCount,
                Status = request.Status,
                HasSeason = request.HasSeason,
            };
        }
        public static MovieGeneralInformationReponse MapToMovieGeneralResponse(this Movie movie)
        {
            return new MovieGeneralInformationReponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                ReleaseDate = movie.ReleaseDate,
                BackdropPath = movie.BackdropPath,
                PosterPath = movie.PosterPath,
                VoteCount = movie.VoteCount,
                path = movie.path,
            };
        }

        public static Movie MovieUpdationRequestToMovie(this Movie movie, MovieUpdationRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                movie.Title = request.Title;
            }

            if (!string.IsNullOrWhiteSpace(request.Overview)) { movie.Overview = request.Overview; }

            if (!string.IsNullOrWhiteSpace(request.PosterPath)) { movie.PosterPath = request.PosterPath; }

            if (!string.IsNullOrWhiteSpace(request.BackdropPath)) { movie.BackdropPath = request.BackdropPath; }

            if (request.ReleaseDate.HasValue) { movie.ReleaseDate = request.ReleaseDate.Value; }

            if (request.Revenue.HasValue) { movie.Revenue = request.Revenue.Value; }

            if (request.Budget.HasValue) { movie.Budget = request.Budget.Value; }

            if (request.VoteCount.HasValue) { movie.VoteCount = request.VoteCount.Value; }

            if (request.Status.HasValue) { movie.Status = request.Status.Value; }

            if (request.HasSeason.HasValue) { movie.HasSeason = request.HasSeason.Value; }

            return movie;
        }
    }
}
