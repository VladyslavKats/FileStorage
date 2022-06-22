using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    public class MailMessage
    {
        public string To { get; set; }
    
        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
