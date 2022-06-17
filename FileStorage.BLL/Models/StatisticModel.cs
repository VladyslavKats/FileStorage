using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    public class StatisticModel
    {
        public string UserName { get; set; }

        public int Files { get; set; }

        public long UsedSpace { get; set; }

        public long MaxSpace { get; set; }
    }
}
