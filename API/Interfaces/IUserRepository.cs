using API.DTOs.Authentication;
using API.DTOs.Users;
using API.Models;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        public Task deleteById(string id);

        public Task<List<User>> findAll();

        public Task<User> getById(string id);

        public Task<User> PutUser(string id, UserUpdationRequest request, string path);

        public Task<User> CreateUser(UserCreationRequest request);

        Task<User> RegisterUserGlobal(RegisterRequest request);

        Task<User?> getByEmail(string email);

        Task AddMovieFavorite(string userId, int movieId);

        Task RemoveMovieFavorite(string userId, int movieId);

        Task<bool> IsMovieFavorite(string userId, int movieId);

        Task<List<Movie>> GetFavoriteMovies(string userId);

        Task UpdateUserScreenTime(string userId, decimal screenTime);

    }
}
