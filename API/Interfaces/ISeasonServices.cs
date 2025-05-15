using API.DTOs;
using API.DTOs.Season;

namespace API.Interfaces
{
    public interface ISeasonServices
    {
        Task<APIresponse<SeasonGeneralInformation>> CreateSeason(SeasonCreationRequest request);
        Task DeleteSeason(int id);
        Task<APIresponse<SeasonGeneralInformation>> GetSeasonGeneralInformationById(int id);
        Task<APIresponse<SeasonGeneralInformation>> UpdateSeason(int id, SeasonUpdationRequest request);
        Task<APIresponse<List<SeasonGeneralInformation>>> GetSeasonsByMovieId(int movieId);
        Task<APIresponse<string>> AddSeasonToMovie(int movieId, int idSeason);
        Task<APIresponse<string>> RemoveSeasonFromMovie(int movieId, int idSeason);
    }
}
