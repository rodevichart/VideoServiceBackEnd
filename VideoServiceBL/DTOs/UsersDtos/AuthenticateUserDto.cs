namespace VideoServiceBL.DTOs.UsersDtos
{
    public class AuthenticateUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}