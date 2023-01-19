using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Options
{
    /// <summary>
    /// Options for email service
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// Host of email service
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Email 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// App password for email
        /// </summary>
        public string Password { get; set; }
    }
}
