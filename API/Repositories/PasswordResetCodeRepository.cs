using API.Data;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PasswordResetCodeRepository
    {
        private readonly AppDbContext _context;
        private Random _random = new Random();

        public PasswordResetCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PasswordResetCode> Create(User user)
        {
            var preCode = await _context.PasswordResetCodes
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (preCode == null)
            {
                preCode = new PasswordResetCode()
                {
                    ExpiredDate = DateTime.UtcNow.AddMinutes(5),
                    Code = _random.Next(1000, 10000).ToString(),
                };

                _context.Entry<User>(user).Entity.PasswordResetCode = preCode;

                await _context.PasswordResetCodes.AddAsync(preCode);
                
            } else
            {
                preCode.Code = _random.Next(1000,10000).ToString();

                preCode.ExpiredDate = DateTime.UtcNow.AddMinutes(5);

                _context.Entry<PasswordResetCode>(preCode).State = EntityState.Modified;
            }

                await SaveChangesAsync();

            return preCode;
        }

        public async Task<PasswordResetCode?> GetPasswordResetAsync(string UserId)
        {
            return await _context.PasswordResetCodes.FirstOrDefaultAsync(p => p.UserId == UserId);
        }


        public async Task SaveChangesAsync()
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
