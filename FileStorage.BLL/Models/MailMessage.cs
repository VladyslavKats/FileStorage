using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Messsage for sending by email
    /// </summary>
    public class MailMessage
    {
        /// <summary>
        /// Email of receiver
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Topic of message
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Body { get; set; }
    }
}
