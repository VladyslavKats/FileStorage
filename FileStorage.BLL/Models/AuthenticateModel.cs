using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Model for authenticate user
    /// </summary>
    public class AuthenticateModel
    {
        /// <summary>
        /// User`s name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User`s password
        /// </summary>
        public string Password { get; set; }
    }
}
