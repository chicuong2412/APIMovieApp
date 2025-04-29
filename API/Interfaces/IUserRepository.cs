using API.DTOs.Users;
using API.Models;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        public Task deleteById(string id);

        public Task<List<User>> findAll();

        public Task<User> getById(string id);

        public Task<User> PutUser(string id, UserUpdationRequest request);

        public Task<User> CreateUser(UserCreationRequest request);
    }
}
