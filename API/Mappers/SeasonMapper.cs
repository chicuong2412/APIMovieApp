using API.DTOs.Season;
using API.Models;

namespace API.Mappers
{
    public static class SeasonMapper
    {
        public static Season MapToSeason(SeasonCreationRequest request)
        {
            return new Models.Season
            {
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                SeasonNumber = request.SeasonNumber,
            };
        }
        public static Season MapToSeason(this Season season, SeasonUpdationRequest request)
        {
            if (request.SeasonNumber.HasValue)
            {
                season.SeasonNumber = request.SeasonNumber.Value;
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                season.Title = request.Title;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                season.Description = request.Description;
            }

            if (request.ReleaseDate.HasValue)
            {
                season.ReleaseDate = request.ReleaseDate.Value;
            }

            return season;
        }

        public static SeasonGeneralInformation SeasonGeneralInformation(this Season season)
        {
            return new SeasonGeneralInformation
            {
                Id = season.Id,
                Title = season.Title,
                Description = season.Description,
                ReleaseDate = season.ReleaseDate,
                SeasonNumber = season.SeasonNumber,
            };
        }
    }
}
