using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Models
{
    /// <summary>
    /// User`s account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// User`s and account id 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Number of user`s files
        /// </summary>
        public int Files { get; set; }
        /// <summary>
        /// Number of bytes which used by account
        /// </summary>
        public long UsedSpace { get; set; }

        /// <summary>
        /// Navigation to user entity
        /// </summary>
        public User User { get; set; }
    }
}
