using API.DTOs.Users;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IUserService
    {
        public Task<APIresponse<UserGlobalReponse>> CreateUser(UserCreationRequest request);

        public Task<APIresponse<UserGlobalReponse>> UpdateUser(string id, UserUpdationRequest request);

        public Task<APIresponse<UserGlobalReponse>> GetUserById(string id);

        public Task<APIresponse<List<UserGlobalReponse>>> GetAllUsers();

        public Task<APIresponse<string>> DeleteById(string id);

    }
}
