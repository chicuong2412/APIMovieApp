using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTOs;
using API.DTOs.Authentication;
using API.ENUMS.ErrorCodes;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using API.Repositories;
using API.ReturnCodes.SuccessCodes;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AuthenticationServices
    {

        private readonly IConfiguration _configuration;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly ExpiredJWTRepository _expiredJWTRepository;

        private readonly IUserRepository _userRepository;

        private readonly PasswordResetCodeRepository _passwordResetCodeRepository;

        private readonly EmailSender _emailSender;

        public AuthenticationServices(IConfiguration configuration,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            ExpiredJWTRepository expiredJWTRepository,
            PasswordResetCodeRepository passwordResetCodeRepository,
            EmailSender emailSender)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _expiredJWTRepository = expiredJWTRepository;
            _passwordResetCodeRepository = passwordResetCodeRepository;
            _emailSender = emailSender;
        }

        public async Task<APIresponse<string>> RegisterUser(RegisterRequest request)
        {
            var user = await _userRepository.RegisterUserGlobal(request);
            if (user is null)
            {
                throw new AppException(ErrorCodes.DataInvalid);
            }


            await _emailSender.SendEmailAsync(request.Email, "Register Successfully", "You just register an account to the Movie Watching App");

            return new APIresponse<string>(SuccessCodes.RegisterSuccessfully);
        }


        private async Task<AuthenticationResponse> GenerateToken(User user)
        {
            var issuer = _configuration["JWT:Issuer"];
            var aduience = _configuration["JWT:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
            var tokenValidityMins = _configuration.GetValue<int>("JWT:ExpireTimeMins");
            var tokenExpiry = DateTime.UtcNow.AddMinutes(tokenValidityMins);
            var calims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("expDate", tokenExpiry.ToString()),
            };

            user.Roles.ToList().ForEach(role =>
            {
                calims.Add(new Claim(ClaimTypes.Role, role.Name!));
                role.Permissions.ToList().ForEach(permission =>
                {
                    calims.Add(new Claim("Permission", permission.Name));
                });
            });

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: aduience,
                claims: calims,
                expires: tokenExpiry,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponse
            {
                Email = user.Email,
                Token = accessToken,
                RefreshToken = await GenerateRefreshToken(user.Id),
                TokenExpiry = (int)tokenExpiry.Subtract(DateTime.UtcNow).TotalMinutes,
            };

        }

        public async Task Logout(string token, string jwt)
        {
            var expiredToken = await _expiredJWTRepository.AddExpiredJWT(jwt);

            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByToken(token);

            if (refreshToken is not null)
            {
                await _refreshTokenRepository.Delete(refreshToken);
            }
        }

        public async Task<AuthenticationResponse?> ValidateUser(string email, string password)
        {
            var user = await _userRepository.getByEmail(email);
            if (user is null)
            {
                return null;
            }

            var passwordHandler = new PasswordHasher<User>();

            var passwordVerificationResult = passwordHandler.VerifyHashedPassword(user, user.PasswordHash!, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await GenerateToken(user);
        }

        public async Task<AuthenticationResponse?> ValidateRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByToken(token);

            if (refreshToken is null || refreshToken.ExpiryDate < DateTime.UtcNow)
            {
                return null;
            }

            var user = await _userRepository.getById(refreshToken.UserId);

            await _refreshTokenRepository.Delete(refreshToken);

            if (user is null)
            {
                throw new AppException(ErrorCodes.DataInvalid);
            }

            return await GenerateToken(user);
        }


        public async Task<APIresponse<string>> ForgotPasswordGetCode(string email)
        {
            var user = await _userRepository.getByEmail(email);

            if (user is null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            var reponse = new APIresponse<string>(SuccessCodes.Success);

            var preCode = await _passwordResetCodeRepository.GetPasswordResetAsync(user.Id);

            if (preCode is not null && Math.Abs(preCode.ExpiredDate.Subtract(DateTime.UtcNow.AddMinutes(5)).TotalMinutes) <= 2)
            {
                throw new AppException(ErrorCodes.RequestSpam);
            }

            var passwordResetCode = await _passwordResetCodeRepository.Create(user);


            await SendEmailPassCode(passwordResetCode, email);

            reponse.message = "Please check email (spam folder) to get the code!!!";

            reponse.data = passwordResetCode.Token;

            return reponse;
        }

        public async Task<APIresponse<string>> ValidateResetCode(string token, string code)
        {

            var resetCode = await _passwordResetCodeRepository.GetPasswordResetByTokenAysnc(token);
            

            if (resetCode is null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }

            if (resetCode.ExpiredDate < DateTime.UtcNow)
            {
                throw new AppException(ErrorCodes.Expired);
            }

            if (resetCode.Code != code)
            {
                throw new AppException(ErrorCodes.DataInvalid);
            }

            resetCode.IsOpenToChange = true;
            resetCode.ExpiredChangePasswor = DateTime.UtcNow.AddMinutes(15);


            await _passwordResetCodeRepository.SaveChangesAsync();

            return new APIresponse<string>(SuccessCodes.Success)
            {
                data = resetCode.Token
            };
        }

        public async Task<APIresponse<string>> ChangePassword(string newPassword, string token)
        {

            var resetCode = await _passwordResetCodeRepository.GetPasswordResetByTokenAysnc(token);

            if (resetCode is null)
            {
                throw new AppException(ErrorCodes.NotFound);
            }
  
            var user = await _userRepository.getById(resetCode.UserId);
            
            if (user is null)
            {
                throw new AppException(ErrorCodes.ServerError);
            }

            if (resetCode.IsOpenToChange && resetCode.ExpiredChangePasswor is not null
                && resetCode.ExpiredChangePasswor > DateTime.UtcNow)
            {
                if (!IsPasswordStrongEnough(newPassword))
                {
                    return new APIresponse<string>(ErrorCodes.DataInvalid);
                }

                var passwordHasher = new PasswordHasher<User>();

                var hashed = passwordHasher.HashPassword(null, newPassword);

                user.PasswordHash = hashed;

                resetCode.IsOpenToChange = false;


                await _passwordResetCodeRepository.SaveChangesAsync();
            }
            else
            {
                return new APIresponse<string>(ErrorCodes.NotFound)
                {
                    data = "Change Password unsuccessfully!!!"
                };
            }

            return new APIresponse<string>(SuccessCodes.Success)
            {
                data = "Change Password Successfully!!!"
            };

        }

        private async Task SendEmailPassCode(PasswordResetCode psc, string email)
        {
            await _emailSender.SendEmailAsync(email, "Password Reset Code", "This is your code: " + psc.Code);
        }


        private bool IsPasswordStrongEnough(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;
            if (!password.Any(char.IsUpper))
                return false;
            if (!password.Any(char.IsLower))
                return false;
            return true;
        }


        private async Task<string> GenerateRefreshToken(string userId)
        {
            var refreshToken = await _refreshTokenRepository.Create(userId);
            return refreshToken.Token;
        }

    }
}
