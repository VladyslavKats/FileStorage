using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Models
{
    public class Account
    {
        public string Id { get; set; }

        public int Files { get; set; }

        public long UsedSpace { get; set; }

        public User User { get; set; }
    }
}
