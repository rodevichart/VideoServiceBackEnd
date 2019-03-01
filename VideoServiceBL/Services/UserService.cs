using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VideoService.VideoServiceBL.Services;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceBL.Enums;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly ICryptService _cryptService;
        private readonly AuthSettings _settings;

        public UserService(VideoServiceDbContext context, IMapper mapper,
            ICryptService cryptService, IOptions<AuthSettings> settings)
            : base(context, mapper)
        {
            _cryptService = cryptService;
            _settings = settings.Value;
        }

        public async Task<UserDto> CreateUserAsync(string userName, string password)
        {
            var userDto = new UserDto
            {
                Role = (byte)Role.User,
                Username = userName,
                Password = _cryptService.EncodePassword(password)
            };

            var user = await TryGetUserAsync(userName);

            if (user != null)
            {
                return null;
            }

            await AddAsync(userDto);

            var createdUser = await TryGetUserAsync(userName);
            createdUser.Password = null;
            return createdUser;
        }

        public async Task<AuthenticateUserDto> AuthenticateAsync(string username, string password)
        {
            var enhancedHashPassword = _cryptService.EncodePassword(password);
            var user = await this.TryGetUserAsync(username); 

            // return null if user not found
            if (user == null || !_cryptService.VerifyPassword(password, enhancedHashPassword))
            {
                return null;
            }

            var roleEnumValue = (Role) user.Role;
            var role = roleEnumValue.ToString();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_settings.LifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var authUser = Mapper.Map<UserDto, AuthenticateUserDto>(user);
            authUser.Token = tokenHandler.WriteToken(token);
            authUser.Password = null;

            return authUser;
        }

        public async Task<UserDto> TryGetUserAsync(string userName)
        {
            var user = await Entities.SingleOrDefaultAsync(e => e.Username.Equals(userName));
            return Mapper.Map<User, UserDto>(user);
        }

    }
}