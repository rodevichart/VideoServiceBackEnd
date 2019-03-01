using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
   
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("/api/authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserDto userData)
        {
            if (string.IsNullOrWhiteSpace(userData.Username) || string.IsNullOrWhiteSpace(userData.Password))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var user = await _userService.AuthenticateAsync(userData.Username, userData.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("/api/registration")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userData)
        {
            if (string.IsNullOrWhiteSpace(userData.Username) || string.IsNullOrWhiteSpace(userData.Password))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            try
            {
                var user = await _userService.CreateUserAsync(userData.Username, userData.Password);
                if (user == null)
                {
                    return BadRequest(new { message = "User is already exists!" });
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/getAllUsers")]
        public IActionResult GetAllAsync()
        {
            var users = _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("/api/authenticate/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}