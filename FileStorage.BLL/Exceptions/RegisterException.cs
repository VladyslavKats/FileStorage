using System;
using System.Runtime.Serialization;

namespace FileStorage.BLL.Exceptions
{
    public class RegisterException : FileStorageException
    {
        public RegisterException()
        {
        }

        public RegisterException(string message) : base(message)
        {
        }

        public RegisterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegisterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
