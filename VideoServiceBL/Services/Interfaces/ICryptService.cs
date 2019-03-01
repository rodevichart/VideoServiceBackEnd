namespace VideoServiceBL.Services.Interfaces
{
    public interface ICryptService
    {
        string EncodePassword(string password);
        bool VerifyPassword(string password, string enhancedHashPassword);
    }
}