using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FileStorage.BLL.Common
{
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
