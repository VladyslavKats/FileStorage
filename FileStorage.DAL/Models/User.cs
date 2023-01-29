
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FileStorage.DAL.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Account id 
        /// </summary>
        public string AccountId { get; set; }
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
