using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FileStorage.PL.Models
{
    /// <summary>
    /// Model for creating document
    /// </summary>
    public class DocumentAddModel
    {
        /// <summary>
        /// Files to add
        /// </summary>
        public IEnumerable<IFormFile> Files { get; set; }

        /// <summary>
        /// Owner name
        /// </summary>
        public string UserName { get; set; }
    }
}
