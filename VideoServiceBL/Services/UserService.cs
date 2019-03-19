using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceBL.Enums;
using VideoServiceBL.Exceptions;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly ICryptService _cryptService;
        private readonly ILogger<UserService> _logger;
        private readonly AuthSettings _settings;

        public UserService(VideoServiceDbContext context,
            ICryptService cryptService, IOptions<AuthSettings> settings, ILogger<UserService> logger, IMapper mapper)
            : base(context, logger, mapper)
        {
            _cryptService = cryptService;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<string> CreateUserAsync(string userName, string name, string password)
        {
            try
            {
                var userFromData = new UserDto
                {
                    Role = (byte) Role.User,
                    Username = userName,
                    Name = name,
                    Password = _cryptService.EncodePassword(password)
                };

                var user = await TryGetUserAsync(userName);

                if (user != null)
                {
                    throw new UserServiceException("User already exists!");
                }

                var createdUser = await AddAsync(userFromData);

                string authUser;
                if (createdUser != null)
                {
                    authUser = await AuthenticateAsync(createdUser.Username, createdUser.Password);
                }
                else
                {
                    throw new UserServiceException("User has not been added! Please try again!");
                }

                return authUser;
            }
            catch (CryptServiceException cryptServiceException)
            {
                throw new BusinessLogicException(cryptServiceException.Message, cryptServiceException);
            }

        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            try
            {
                var enhancedHashPassword = _cryptService.EncodePassword(password);
                var user = await this.TryGetUserAsync(username);
                var isPasswordVerified = _cryptService.VerifyPassword(password, enhancedHashPassword);

                // return null if user not found
                if (user == null || !isPasswordVerified)
                {
                    throw new UserServiceException("User or password is incorrect");
                }

                return GetAuthenticateUserWithTokenAsync(user);
            }
            catch (CryptServiceException cryptServiceException)
            {
                throw new BusinessLogicException(cryptServiceException.Message, cryptServiceException);
            }
        }

        private string GetAuthenticateUserWithTokenAsync(User user)
        {
            var roleEnumValue = (Role) user.Role;
            var role = roleEnumValue.ToString();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetJwtSecurityTokenHandler(user, role);
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetJwtSecurityTokenHandler(User user, string role)
        {
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Username),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_settings.LifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private async Task<User> TryGetUserAsync(string userName)
        {
            try
            {
                return await Entities.SingleOrDefaultAsync(e => e.Username.Equals(userName));
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could not get the user", ex);
                throw new BusinessLogicException("Could not get the user", ex);
            }
        }

    }
}

