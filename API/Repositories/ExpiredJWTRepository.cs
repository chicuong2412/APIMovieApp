using System.IdentityModel.Tokens.Jwt;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ExpiredJWTRepository
    {

        private readonly AppDbContext _context;
        public ExpiredJWTRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<LogoutJWT> AddExpiredJWT(string jwt)
        {
            var jwtDecoded = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

            var expiryDate = jwtDecoded.Claims.FirstOrDefault(x => x.Type == "expDate")?.Value;

            var token = new LogoutJWT()
            {
                Token = jwt,
                ExpiryDate = DateTime.Parse(expiryDate!),
            };

            await _context.LogoutJWTs.AddAsync(token);

            await SaveChangesAsync();

            return token;
        }

        public async Task<bool> IsTokenExpired(string token)
        {
            var expiredToken = await _context.LogoutJWTs.FirstOrDefaultAsync(x => x.Token == token);
            if (expiredToken == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteExpired()
        {
            var expiredTokens = await _context.LogoutJWTs.Where(x => x.ExpiryDate < DateTime.UtcNow).ToListAsync();
            if (expiredTokens.Count == 0)
            {
                return false;
            }
            _context.LogoutJWTs.RemoveRange(expiredTokens);
            await SaveChangesAsync();
            return true;
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception)
            {

            }
        }

    }
}
