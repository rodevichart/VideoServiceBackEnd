using System.ComponentModel.DataAnnotations;

namespace VideoServiceBL.DTOs.UsersDtos
{
    public class AuthenticatedUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}