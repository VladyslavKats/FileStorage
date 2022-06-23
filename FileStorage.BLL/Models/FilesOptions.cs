using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Contains configuration for files settings
    /// </summary>
    public class FilesOptions
    {
 
        /// <summary>
        /// Name for directory with files
        /// </summary>
        public string DirectiveName { get; set; }
        /// <summary>
        /// Max space for user
        /// </summary>
        public long MaxSizeSpace { get; set; }
        /// <summary>
        /// Total space for files
        /// </summary>
        public long TotalSpace { get; set; }
    }
}
