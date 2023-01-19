using System;
using System.Runtime.Serialization;

namespace FileStorage.BLL.Exceptions
{
    internal class ConfirmationException : FileStorageException
    {
        public ConfirmationException()
        {
        }

        public ConfirmationException(string message) : base(message)
        {
        }

        public ConfirmationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfirmationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
