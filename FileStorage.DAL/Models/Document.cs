using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Models
{
    /// <summary>
    /// Class describes file
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Document id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Document name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Document size in bytes
        /// </summary>
        public long Size { get; set; }
        
        /// <summary>
        /// Type of document
        /// </summary>
        public string ContentType { get; set; }
        
        /// <summary>
        /// Document`s path 
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Owner id 
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Owner
        /// </summary>
        public User  User { get; set; }
    }
}
