using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{

    /// <summary>
    /// Model for response after authenticate
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// User`s name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Jwt token 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// User`s id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Is it administrator or not
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
