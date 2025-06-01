using API.DTOs.Users;
using API.DTOs;
using API.Models;
using API.DTOs.Movies;

namespace API.Interfaces
{
    public interface IUserService
    {
        public Task<APIresponse<UserGlobalReponse>> CreateUser(UserCreationRequest request);

        public Task<APIresponse<UserGlobalReponse>> UpdateUser(string id, UserUpdationRequest request);

        public Task<APIresponse<UserGlobalReponse>> GetUserById(string id);

        public Task<APIresponse<List<UserGlobalReponse>>> GetAllUsers();

        public Task<APIresponse<string>> DeleteById(string id);

        Task AddMovieFavorite(string userId, int movieId);

        Task RemoveMovieFavorite(string userId, int movieId);

        Task<APIresponse<bool>> IsMovieFavorite(string userId, int movieId);

        Task<APIresponse<List<MovieGeneralInformationReponse>>> GetFavoriteMovies(string userId);

        Task UpdateUserScreenTime(string userId, decimal screenTime);

    }
}
