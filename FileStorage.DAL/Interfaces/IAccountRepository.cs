using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    /// <summary>
    /// Contains all basic methods for manage accounts
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Creates account for user
        /// </summary>
        /// <param name="userId">User`s id</param>
        /// <returns></returns>
        Task CreateAsync(string userId);

        /// <summary>
        /// Updates account data 
        /// </summary>
        /// <param name="account">Account with changes</param>
        /// <returns>Current account after changes</returns>
        Task<Account> UpdateAsync(Account account);

        /// <summary>
        /// Deletes account of user
        /// </summary>
        /// <param name="id">User`s id to delete</param>
        /// <returns></returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Returns account 
        /// </summary>
        /// <param name="id">Account`s id to return</param>
        /// <returns></returns>
        Task<Account> GetByIdAsync(string id);

        /// <summary>
        /// Returns all accounts 
        /// </summary>
        /// <returns>All accounts </returns>
        Task<IEnumerable<Account>> GetAllAsync();

        /// <summary>
        /// Save all changes
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
