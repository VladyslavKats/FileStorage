using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    public class TotalStatisticModel
    {
        public int TotalFiles { get; set; }

        public long TotalUsedSpace { get; set; }

        public long MaxSpace { get; set; }
    }
}
