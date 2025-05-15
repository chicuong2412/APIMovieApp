using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Users;
using API.Interfaces;
using API.Mappers;
using API.Models;
using API.Repositories;
using API.ReturnCodes.SuccessCodes;

namespace API.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<APIresponse<UserGlobalReponse>> CreateUser(UserCreationRequest request)
        {
            var user = await _userRepository.CreateUser(request);
            var response = new APIresponse<UserGlobalReponse>(SuccessCodes.Created);

            response.data = user.ToUserGlobalReponse();

            return response;
        }

        public async Task<APIresponse<UserGlobalReponse>> UpdateUser(string id, UserUpdationRequest request)
        {
            var user = await _userRepository.PutUser(id, request);
            var reponse = new APIresponse<UserGlobalReponse>(SuccessCodes.Success);
            reponse.data = user.ToUserGlobalReponse();
            return reponse;
        }

        public async Task<APIresponse<UserGlobalReponse>> GetUserById(string id)
        {
            var user = await _userRepository.getById(id);
            var reponse = new APIresponse<UserGlobalReponse>(SuccessCodes.Success);
            reponse.data = user.ToUserGlobalReponse();
            return reponse;
        }

        public async Task<APIresponse<List<UserGlobalReponse>>> GetAllUsers()
        {
            var reponse = new APIresponse<List<UserGlobalReponse>>(SuccessCodes.Success);

            var users = await _userRepository.findAll();

            reponse.data = users.Select(user => user.ToUserGlobalReponse()).ToList();

            return reponse;
        }

        public async Task<APIresponse<string>> DeleteById(string id)
        {
            await _userRepository.deleteById(id);
            var reponse = new APIresponse<String>(SuccessCodes.Success);
            reponse.data = "User has been Deleted";
            return reponse;
        }
    }
}
