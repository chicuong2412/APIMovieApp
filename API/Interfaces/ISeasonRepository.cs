using API.DTOs;
using API.DTOs.Season;
using API.Models;

namespace API.Interfaces
{
    public interface ISeasonRepository
    {
        Task<Season> GetSeasonById(int id);
        Task<Season> CreateSeason(SeasonCreationRequest request);
        Task<Season> UpdateSeason(int id, SeasonUpdationRequest request);
        Task<List<Season>> GetSeasonsByMovieId(int movieId);
        Task<List<Season>> GetAllSeasonsWithEpisodesByMovieId(int movieId);
        Task<Season> GetSeasonById(int id, bool includeEpisodes);
        Task AddSeasonToMovie(int movieId, int idSeason);
        Task RemoveSeasonFromMovie(int movieId, int idSeason);
        Task DeleteSeason(int id);

    }
}
