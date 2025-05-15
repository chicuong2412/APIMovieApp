using API.Models;

namespace API.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokens> Create(string userId);

        Task<RefreshTokens?> GetRefreshTokenByToken(string token);

        Task<RefreshTokens?> GetRefreshTokenByUserId(string userId);

        Task Delete(RefreshTokens refreshToken);

        Task<List<RefreshTokens>> GetExpiredRefreshTokens();

    }
}
