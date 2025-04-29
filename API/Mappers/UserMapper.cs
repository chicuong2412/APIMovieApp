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
            };
        }

        public static void UpdateUser(User user, UserUpdationRequest request)
        {
            if (request != null)
            {
                if (request.Address != null)
                {
                    user.Address = request.Address;
                }
                if (request.Age != null)
                {
                    user.Age = request.Age;
                }
                if (request.Email != null)
                {
                    user.Email = request.Email;
                }
                if (request.Name != null)
                {
                    user.Name = request.Name;
                }
                if (request.PhoneNumber != null)
                {
                    user.PhoneNumber = request.PhoneNumber;
                }
                if (request.DoB != null)
                {
                    user.DoB = request.DoB;
                }
                if (request.Username != null)
                {
                    user.UserName = request.Username;
                }
            }

        }
    }
}
