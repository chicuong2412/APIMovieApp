using API.DTOs;
using API.DTOs.Episode;
using API.Interfaces;
using API.Mappers;
using API.ReturnCodes.SuccessCodes;

namespace API.Services
{
    public class EpisodeServices : IEpisodeServices
    {
        private readonly IEpisodeRepository _episodeRepository;
        public EpisodeServices(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }
        
        public async Task<APIresponse<EpisodeGeneralReponse>> GetEpisodeById(int id)
        {
            var episode = await _episodeRepository.GetEpisodeByIdAsync(id);
            var response = new APIresponse<EpisodeGeneralReponse>(SuccessCodes.Success);

            response.data = episode.ToGeneralReponse();

            return response;
        }

        public async Task<APIresponse<IEnumerable<EpisodeGeneralReponse>>> GetEpisodesBySeasonId(int seasonId, ObjectFilter objectFilter)
        {
            var episodes = await _episodeRepository.GetEpisodesBySeasonIdAsync(seasonId, objectFilter);
            var response = new APIresponse<IEnumerable<EpisodeGeneralReponse>>(SuccessCodes.Success);
            response.data = episodes.Select(e => e.ToGeneralReponse()).ToList();
            return response;
        }

        public async Task<APIresponse<IEnumerable<EpisodeGeneralReponse>>> GetAllEpisodes()
        {
            var episodes = await _episodeRepository.GetAllEpisodesAsync();
            var response = new APIresponse<IEnumerable<EpisodeGeneralReponse>>(SuccessCodes.Success);
            response.data = episodes.Select(e => e.ToGeneralReponse()).ToList();
            return response;
        }

        public async Task<APIresponse<EpisodeGeneralReponse>> CreateEpisode(EpisodeCreationRequest request)
        {
            var episode = await _episodeRepository.AddEpisodeAsync(request);
            var response = new APIresponse<EpisodeGeneralReponse>(SuccessCodes.Created);
            response.data = episode.ToGeneralReponse();
            return response;
        }

        public async Task<APIresponse<EpisodeGeneralReponse>> UpdateEpisode(int id, EpisodeUpdationRequest request)
        {
            var episode = await _episodeRepository.UpdateEpisodeAsync(id, request);
            var response = new APIresponse<EpisodeGeneralReponse>(SuccessCodes.Success);
            response.data = episode.ToGeneralReponse();
            return response;
        }

        public async Task<APIresponse<string>> DeleteEpisode(int id)
        {
            await _episodeRepository.DeleteEpisodeAsync(id);
            var response = new APIresponse<string>(SuccessCodes.NoContent);
            response.data = "Episode deleted successfully.";
            return response;
        }

        public async Task<APIresponse<string>> AddEpisodeToSeason(int seasonId, int episodeId)
        {
            var response = new APIresponse<string>(SuccessCodes.Success);
            await _episodeRepository.AddEpisodeToSeason(seasonId, episodeId);
            response.data = "Episode added to season successfully.";
            return response;
        }

        public async Task<APIresponse<string>> RemoveEpisodeFromSeason(int seasonId, int episodeId)
        {
            var response = new APIresponse<string>(SuccessCodes.Success);
            await _episodeRepository.RemoveEpisodeFromSeason(seasonId, episodeId);
            response.data = "Episode removed from season successfully.";
            return response;
        }




    }
}
