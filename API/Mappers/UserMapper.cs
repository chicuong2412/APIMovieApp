using API.DTOs.Users;
using API.Models;

namespace API.Mappers
{
    public static class UserMapper
    {

        public static User CreateUser(UserCreationRequest request)
        {
            return new User
            {
                Address = request.Address,
                Age = request.Age,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                DoB = request.DoB,
                UserName = request.Username,
                PasswordHash = request.Password,
            };
        }

        public static UserGlobalReponse ToUserGlobalReponse(this User user)
        {
            return new UserGlobalReponse
            {
                Username = user.UserName,
                Address = user.Address,
                Age = user.Age,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                DoB = user.DoB,
                Id = user.Id,
                ScreenTime = user.ScreenTime,
                Avatar = user.Avatar
            };
        }

        public static void UpdateUser(User user, UserUpdationRequest request)
        {
            if (request != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Address))
                {
                    user.Address = request.Address;
                }
                if (request.Age.HasValue)
                {
                    user.Age = request.Age.Value;
                }
                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    user.Email = request.Email;
                }
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    user.Name = request.Name;
                }
                if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                {
                    user.PhoneNumber = request.PhoneNumber;
                }
                if (request.DoB.HasValue)
                {
                    user.DoB = request.DoB.Value;
                }
                if (!string.IsNullOrWhiteSpace(request.Username))
                {
                    user.UserName = request.Username;
                }
            }

        }
    }
}
