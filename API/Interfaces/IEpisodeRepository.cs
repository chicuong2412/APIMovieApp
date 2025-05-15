using API.DTOs;
using API.DTOs.Episode;
using API.Models;

namespace API.Interfaces
{
    public interface IEpisodeRepository
    {
        Task<Episode> GetEpisodeByIdAsync(int id);
        Task<IEnumerable<Episode>> GetEpisodesBySeasonIdAsync(int seasonId, ObjectFilter objectFilter);
        Task<IEnumerable<Episode>> GetAllEpisodesAsync();
        Task<Episode> AddEpisodeAsync(EpisodeCreationRequest request);
        Task<Episode> UpdateEpisodeAsync(int id, EpisodeUpdationRequest request);
        Task DeleteEpisodeAsync(int id);

        Task AddEpisodeToSeason(int seasonId, int episodeId);

        Task RemoveEpisodeFromSeason(int seasonId, int episodeId);
    }
}
