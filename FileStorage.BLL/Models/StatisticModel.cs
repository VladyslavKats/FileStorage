using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Statistic model
    /// </summary>
    public class StatisticModel
    {
        /// <summary>
        /// User`s name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Number of user`s files
        /// </summary>
        public int Files { get; set; }
        /// <summary>
        /// Number of user`s space is used in bytes
        /// </summary>
        public long UsedSpace { get; set; }
        /// <summary>
        /// Max number of used space by user
        /// </summary>
        public long MaxSpace { get; set; }
    }
}
