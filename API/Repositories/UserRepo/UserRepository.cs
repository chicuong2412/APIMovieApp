using API.Data;
using API.DTOs.Users;
using API.ENUMS.ErrorCodes;
using API.Exception;
using API.Interfaces;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task deleteById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> findAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> getById(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return user;
        }

        public async Task<User> PutUser(string id, UserUpdationRequest request)
        {
            if (!this.UserExists(id))
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null) throw new AppException(ErrorCodes.NotFound);

            UserMapper.UpdateUser(user, request);


            return user;
        }

        public async Task<User> CreateUser(UserCreationRequest request)
        {
            var user = UserMapper.CreateUser(request);
            
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    throw new AppException(ErrorCodes.Conflict);
                }
                else
                {
                    throw new AppException(ErrorCodes.ServerError);
                }
            }

            return user;
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
