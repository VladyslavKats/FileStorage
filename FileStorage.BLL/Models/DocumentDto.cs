using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Defines document
    /// </summary>
    public class DocumentDto
    {
        /// <summary>
        /// Document`s id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Document`s name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Document`s size in bytes
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Type of document
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Path to document
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Owner`s id
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Owner
        /// </summary>
        public User User { get; set; }
    }
}
