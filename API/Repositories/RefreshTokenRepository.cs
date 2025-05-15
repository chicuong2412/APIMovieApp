using API.Data;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<RefreshTokens> Create(string userId)
        {
            var token = new RefreshTokens()
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:RefreshExpireTimeMins")),
            };

            await _context.RefreshTokens.AddAsync(token);

            await SaveChangesAsync();

            return token;
        }


        public async Task<RefreshTokens?> GetRefreshTokenByToken(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(token));
            
            if (refreshToken == null)
            {
                return null;
            }


            return refreshToken;
        }

        public async Task<RefreshTokens?> GetRefreshTokenByUserId(string userId)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);

            if (refreshToken == null)
            {
                return null;
            }
            return refreshToken;
        }

        public async Task Delete(RefreshTokens refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);

            await SaveChangesAsync();
        }

        public async Task<List<RefreshTokens>> GetExpiredRefreshTokens()
        {
            var expiredTokens = await _context.RefreshTokens
                .Where(t => t.ExpiryDate < DateTime.UtcNow)
                .ToListAsync();
            return expiredTokens;
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
