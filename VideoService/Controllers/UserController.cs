using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
    [Route("/api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticatedUserDto userData)
        {
            var jwtToken = await _userService.AuthenticateAsync(userData.Username, userData.Password);
            return Ok(jwtToken);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userData)
        {
            var jwtToken = await _userService.CreateUserAsync(userData.Username, userData.Name, userData.Password);
            return Ok(jwtToken);
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetAsync(id);
            return Ok(user);
        }
    }
}