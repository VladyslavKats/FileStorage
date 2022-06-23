using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FileStorage.BLL.Common
{
    /// <summary>
    /// Represents errors that occur during file storage authenticate  execution
    /// </summary>
    public class FileStorageAuthenticateException : FileStorageException
    {
        public FileStorageAuthenticateException()
        {
        }

        public FileStorageAuthenticateException(string message) : base(message)
        {
        }

        public FileStorageAuthenticateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileStorageAuthenticateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
