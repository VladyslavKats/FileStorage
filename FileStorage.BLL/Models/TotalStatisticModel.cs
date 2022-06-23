using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Total statistic of file storage
    /// </summary>
    public class TotalStatisticModel
    {
        /// <summary>
        /// Number of files in storage
        /// </summary>
        public int TotalFiles { get; set; }
        /// <summary>
        /// Number of space is used in bytes
        /// </summary>
        public long TotalUsedSpace { get; set; }
        /// <summary>
        /// Max number of space in bytes
        /// </summary>
        public long MaxSpace { get; set; }
    }
}
