using API.DTOs;
using API.DTOs.Episode;

namespace API.Interfaces
{
    public interface IEpisodeServices
    {
        Task<APIresponse<EpisodeGeneralReponse>> GetEpisodeById(int id);
        Task<APIresponse<IEnumerable<EpisodeGeneralReponse>>> GetEpisodesBySeasonId(int seasonId, ObjectFilter objectFilter);
        Task<APIresponse<IEnumerable<EpisodeGeneralReponse>>> GetAllEpisodes();
        Task<APIresponse<EpisodeGeneralReponse>> CreateEpisode(EpisodeCreationRequest request);
        Task<APIresponse<EpisodeGeneralReponse>> UpdateEpisode(int id, EpisodeUpdationRequest request);
        Task<APIresponse<string>> DeleteEpisode(int id);

        Task<APIresponse<string>> AddEpisodeToSeason(int seasonId, int episodeId);

        Task<APIresponse<string>> RemoveEpisodeFromSeason(int seasonId, int episodeId);
    }
}
