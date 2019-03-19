using System;
using Microsoft.Extensions.Logging;
using VideoServiceBL.Exceptions;
using VideoServiceBL.Services.Interfaces;

namespace VideoServiceBL.Services
{
    public class CryptService : ICryptService
    {
        private readonly ILogger<CryptService> _logger;

        public CryptService(ILogger<CryptService> logger)
        {
            _logger = logger;
        }

        public string EncodePassword(string password)
        {
            try
            {
                var encodePassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
                return encodePassword;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get hash password!" , ex);
                throw new CryptServiceException("Could not get hash password!", ex);
            }
           
        }

        public bool VerifyPassword(string password, string enhancedHashPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.EnhancedVerify(password, enhancedHashPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not verify password!", ex);
                throw new CryptServiceException("Could not verify password!", ex);
            }
        }
    }
}