using VideoServiceBL.Services.Interfaces;

namespace VideoServiceBL.Services
{
    public class CryptService : ICryptService
    {
        public string EncodePassword(string password)
        {
            var encodePassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return encodePassword;
        }

        public bool VerifyPassword(string password, string enhancedHashPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, enhancedHashPassword);
        }
    }
}