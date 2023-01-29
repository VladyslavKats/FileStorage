using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FileStorage.BLL.Exceptions
{
    /// <summary>
    /// Represents errors that occur during file storage authenticate  execution
    /// </summary>
    public class AuthenticateException : FileStorageException
    {
        public AuthenticateException()
        {
        }

        public AuthenticateException(string message) : base(message)
        {
        }

        public AuthenticateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthenticateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
