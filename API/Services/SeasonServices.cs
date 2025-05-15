using API.DTOs;
using API.DTOs.Season;
using API.Interfaces;
using API.Mappers;
using API.ReturnCodes.SuccessCodes;

namespace API.Services
{
    public class SeasonServices : ISeasonServices
    {
        private readonly ISeasonRepository _seasonRepository;
        public SeasonServices(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }
        public async Task<APIresponse<SeasonGeneralInformation>> CreateSeason(SeasonCreationRequest request)
        {
            var season = await _seasonRepository.CreateSeason(request);
            var reponse = new APIresponse<SeasonGeneralInformation>(SuccessCodes.Created);
            reponse.data = season.SeasonGeneralInformation();
            return reponse;
        }
        public async Task DeleteSeason(int id)
        {
            await _seasonRepository.DeleteSeason(id);
        }
        public async Task<APIresponse<SeasonGeneralInformation>> GetSeasonGeneralInformationById(int id)
        {
            var season = await _seasonRepository.GetSeasonById(id);
            var response = new APIresponse<SeasonGeneralInformation>(SuccessCodes.Success);
            response.data = season.SeasonGeneralInformation();
            return response;
        }
        public async Task<APIresponse<SeasonGeneralInformation>> UpdateSeason(int id, SeasonUpdationRequest request)
        {
            var season = await _seasonRepository.UpdateSeason(id, request);
            var response = new APIresponse<SeasonGeneralInformation>(SuccessCodes.Success);
            response.data = season.SeasonGeneralInformation();
            return response;
        }
        
        public async Task<APIresponse<List<SeasonGeneralInformation>>> GetSeasonsByMovieId(int movieId)
        {
            var seasons = await _seasonRepository.GetSeasonsByMovieId(movieId);
            var response = new APIresponse<List<SeasonGeneralInformation>>(SuccessCodes.Success);
            response.data = seasons.Select(s => s.SeasonGeneralInformation()).ToList();
            return response;
        }

        public async Task<APIresponse<string>> AddSeasonToMovie(int movieId, int idSeason)
        {
            await _seasonRepository.AddSeasonToMovie(movieId, idSeason);
            var response = new APIresponse<string>(SuccessCodes.Success);
            response.data = "Season added to movie";
            return response;
        }

        public async Task<APIresponse<string>> RemoveSeasonFromMovie(int movieId, int idSeason)
        {
            await _seasonRepository.RemoveSeasonFromMovie(movieId, idSeason);
            var response = new APIresponse<string>(SuccessCodes.Success);
            response.data = "Season removed from movie";
            return response;
        }
    }
}
