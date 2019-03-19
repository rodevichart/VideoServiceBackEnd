﻿using System;
using System.Runtime.Serialization;

namespace VideoServiceBL.Exceptions
{
    [Serializable]
    public class UserServiceException: Exception
    {
        public UserServiceException()
        {
        }

        public UserServiceException(string message)
            : base(message)
        {
        }

        public UserServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}