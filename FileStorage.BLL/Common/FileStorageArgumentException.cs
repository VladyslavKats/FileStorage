using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FileStorage.BLL.Common
{
    public class FileStorageArgumentException : FileStorageException
    {
        public FileStorageArgumentException()
        {
        }

        public FileStorageArgumentException(string message) : base(message)
        {
        }

        public FileStorageArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileStorageArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
