
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User`s account
        /// </summary>
        public Account Account { get; set; }
        /// <summary>
        /// All user`s documents
        /// </summary>
        public ICollection<Document> Documents { get; set; }
    }
}
