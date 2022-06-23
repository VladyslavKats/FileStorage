using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    /// <summary>
    /// Model for user registration 
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// User`s name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User`s password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User`s email
        /// </summary>
        public string Email { get; set; }
    }
}
