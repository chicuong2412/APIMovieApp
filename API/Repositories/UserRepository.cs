using API.Data;
using API.DTOs.Authentication;
using API.DTOs.Users;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace API.Repositories
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

            user.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task AddMovieFavorite(string userId, int movieId)
        {
            var user = await _context.Users
                .Include(user => user.FavoriteMovies)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return;
            }
            if (!user.FavoriteMovies.Contains(movie))
            {
                user.FavoriteMovies.Add(movie);
                await SaveChangesAsync();
            }
        }

        public async Task RemoveMovieFavorite(string userId, int movieId)
        {
            var user = await _context.Users
                .Include(user => user.FavoriteMovies)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return;
            }
            if (user.FavoriteMovies.Contains(movie))
            {
                user.FavoriteMovies.Remove(movie);
                await SaveChangesAsync();
            }
        }

        public async Task<List<Movie>> GetFavoriteMovies(string userId)
        {
            var user = await _context.Users
                .Include(user => user.FavoriteMovies)
                    .ThenInclude(movie => movie.Generes)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            return user.FavoriteMovies.ToList();
        }

        public async Task<bool> IsMovieFavorite(string userId, int movieId)
        {
            var user = await _context.Users
                .Include(user => user.FavoriteMovies)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return user.FavoriteMovies.Any(movie => movie.Id == movieId);
        }

        public async Task UpdateUserScreenTime(string userId, decimal screenTime)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
            user.ScreenTime += screenTime;
            await SaveChangesAsync();
        }

        public async Task<List<User>> findAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> getByEmail(string email)
        {
            var user = await _context.Users
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User> getById(string id)
        {
            var user = await _context.Users
            .Include(x => x.Roles)
                    .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            return user;
        }

        public async Task<User> PutUser(string id, UserUpdationRequest request, string path)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) throw new AppException(ErrorCodes.NotFound);

            if (!string.IsNullOrWhiteSpace(request.Email)
                && !request.Email.Equals(user.Email)
                && UserExistsByEmail(request.Email))
            {
                throw new AppException(ErrorCodes.EmailExisted);
            }

            if (!string.IsNullOrWhiteSpace(request.Username)
                && !request.Username.Equals(user.UserName)
                && UserExistsByUsername(request.Username))
            {
                throw new AppException(ErrorCodes.UsernameExisted);
            }

            UserMapper.UpdateUser(user, request);
                
            if (!string.IsNullOrWhiteSpace(path))
            {
                user.Avatar = path;
            }

            await SaveChangesAsync();

            return user;
        }

        public async Task<User> CreateUser(UserCreationRequest request)
        {
            if (UserExistsByEmail(request.Email))
            {
                throw new AppException(ErrorCodes.EmailExisted);
            }

            if (UserExistsByUsername(request.Username))
            {
                throw new AppException(ErrorCodes.UsernameExisted);
            }

            var user = UserMapper.CreateUser(request);

            var passwordHasher = new PasswordHasher<object>();

            var passwordHash = passwordHasher.HashPassword(null, request.Password);



            await _context.Users.AddAsync(user);

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "User");

            if (role is not null)
            {
                user.Roles.Add((Role)role);
            }


            try
            {
                await SaveChangesAsync();
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

        public async Task<User> RegisterUserGlobal(RegisterRequest request)
        {
            if (UserExistsByEmail(request.Email))
            {
                throw new AppException(ErrorCodes.EmailExisted);
            }

            var passwordHasher = new PasswordHasher<object>();

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHasher.HashPassword(null, request.Password),
            };

            await _context.Users.AddAsync(user);

            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Name == "User");

            if (role != null)
            {
                user.Roles.Add((Role) role);
            }

            await SaveChangesAsync();

            return user;
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool UserExistsByEmail(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        private bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(e => e.UserName == username);
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new AppException(ErrorCodes.ServerError);
            }
        }

    }
}
