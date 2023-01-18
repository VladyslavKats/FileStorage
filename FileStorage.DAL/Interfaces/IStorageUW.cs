using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    /// <summary>
    /// Contains repositories for manage file storage
    /// </summary>
    public interface IStorageUW : IDisposable
    {
        /// <summary>
        /// Manages users
        /// </summary>
        UserManager<User> Users { get; }
        /// <summary>
        /// Manages roles
        /// </summary>
        RoleManager<IdentityRole> Roles { get; } 
        /// <summary>
        /// Manages documents
        /// </summary>
        IRepository<string,Document> Documents { get; }
        /// <summary>
        /// Manages accounts 
        /// </summary>
        IRepository<string,Account> Accounts { get; }
        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}
