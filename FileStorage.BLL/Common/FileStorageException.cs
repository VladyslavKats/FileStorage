using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FileStorage.BLL.Common
{
    /// <summary>
    /// Represents errors that occur during file storage execution
    /// </summary>
    [Serializable]
    public class FileStorageException : Exception
    {
        public FileStorageException()
        {
        }

        public FileStorageException(string message) : base(message)
        {
        }

        public FileStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
