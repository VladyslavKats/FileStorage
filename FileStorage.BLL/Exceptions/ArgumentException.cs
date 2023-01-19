using System;
using System.Runtime.Serialization;

namespace FileStorage.BLL.Exceptions
{
    public class ArgumentException : FileStorageException
    {
        public ArgumentException()
        {
        }

        public ArgumentException(string message) : base(message)
        {
        }

        public ArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
