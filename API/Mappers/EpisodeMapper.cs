using API.DTOs.Episode;
using API.Models;

namespace API.Mappers
{
    public static class EpisodeMapper
    {
        public static Episode CreationToEpisode(EpisodeCreationRequest request)
        {
            return new Episode
            {
                EpisodeNumber = request.EpisodeNumber,
                Overview = request.Overview,
                Title = request.Title,
                Path = request.Path,
                PosterPath = request.PosterPath,
            };
        }

        public static Episode UpdationToEpisode(this Episode episode, EpisodeUpdationRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                episode.Title = request.Title;
            }

            if (!string.IsNullOrWhiteSpace(request.Path)) { episode.Path = request.Path; }

            if (!string.IsNullOrEmpty(request.PosterPath))
            {
                episode.PosterPath = request.PosterPath;
            }

            if (!string.IsNullOrWhiteSpace(request.Overview))
            {
                episode.Overview = request.Overview;
            }

            if (request.EpisodeNumber.HasValue)
            {
                episode.EpisodeNumber = request.EpisodeNumber.Value;
            }

            return episode;
        }

        public static EpisodeGeneralReponse ToGeneralReponse(this Episode episode)
        {
            return new EpisodeGeneralReponse()
            {
                EpisodeNumber = episode.EpisodeNumber,
                Title = episode.Title,
                Path = episode.Path,
                Overview = episode.Overview,
                PosterPath = episode.PosterPath,
                SeasonId = episode.SeasonId,
            };
        }

    }
}
