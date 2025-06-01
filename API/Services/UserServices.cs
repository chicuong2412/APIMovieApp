using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Movies;
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
        private readonly FileServices _fileServices;
        public UserServices(IUserRepository userRepository, FileServices fileServices)
        {
            this._userRepository = userRepository;
            _fileServices = fileServices;
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
            string path = null;
            if (request.Avatar != null)
            {
                path = await _fileServices.SaveFileAvatarAsync(request.Avatar);
            }
            var user = await _userRepository.PutUser(id, request, path);
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


        public async Task AddMovieFavorite(string userId, int movieId)
        {
            await _userRepository.AddMovieFavorite(userId, movieId);
        }

        public async Task RemoveMovieFavorite(string userId, int movieId)
        {
            await _userRepository.RemoveMovieFavorite(userId, movieId);

        }

        public async Task<APIresponse<bool>> IsMovieFavorite(string userId, int movieId)
        {
            var rp = new APIresponse<bool>(SuccessCodes.Success);

            rp.data = await _userRepository.IsMovieFavorite(userId, movieId);

            return rp;
        }

        public async Task<APIresponse<List<MovieGeneralInformationReponse>>> GetFavoriteMovies(string userId)
        {
            var rp = new APIresponse<List<MovieGeneralInformationReponse>>(SuccessCodes.Success);

            rp.data = (await _userRepository.GetFavoriteMovies(userId)).Select(movie => movie.MapToMovieGeneralResponse()).ToList();
            return rp;
        }

        public async Task UpdateUserScreenTime(string userId, decimal screenTime)
        {
            await _userRepository.UpdateUserScreenTime(userId, screenTime);
        }
    }
}
