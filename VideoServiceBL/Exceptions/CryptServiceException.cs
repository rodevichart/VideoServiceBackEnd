using System;
using System.Runtime.Serialization;

namespace VideoServiceBL.Exceptions
{
    [Serializable]
    public class CryptServiceException: BusinessLogicException
    {
        public CryptServiceException()
        {
        }

        public CryptServiceException(string message)
            : base(message)
        {
        }

        public CryptServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CryptServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}