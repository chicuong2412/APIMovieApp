
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;

namespace API.SheduleServices
{
    public class CleanRefreshTokenService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public CleanRefreshTokenService(ILogger<CleanRefreshTokenService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _scopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var refreshTokenRepository = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();
                var expiredTokens = await refreshTokenRepository.GetExpiredRefreshTokens();
                if (expiredTokens != null && expiredTokens.Count > 0)
                {
                    foreach (var token in expiredTokens)
                    {
                        await refreshTokenRepository.Delete(token);
                    }
                }
                _logger.LogInformation("Expired refresh tokens cleaned up.");
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
