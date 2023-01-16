using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Repositories
{

    /// <summary>
    /// Class for managing accounts
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly FileStorageContext _context;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="context">Database context</param>
        public AccountRepository(FileStorageContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates account for user
        /// </summary>
        /// <param name="userId">User`s id</param>
        /// <returns></returns>
        public async Task CreateAsync(string userId)
        {
            await Task.Run(() => _context.Accounts.Add(new Account { Id = userId, Files = 0, UsedSpace = 0 }));
        }
        /// <summary>
        /// Deletes account 
        /// </summary>
        /// <param name="id">Account`s id to delete</param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            await Task.Run(() => _context.Accounts.Remove(new Account { Id = id }));
        }

        /// <summary>
        /// Returns all accounts
        /// </summary>
        /// <returns>All accounts</returns>
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(a => a.User).ToArrayAsync();
        }
        /// <summary>
        /// Returns account by id 
        /// </summary>
        /// <param name="id">Account`s id to return</param>
        /// <returns>Account</returns>
        public async Task<Account> GetByIdAsync(string id)
        {
            return await _context.Accounts.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }
        /// <summary>
        /// Save changes 
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates account
        /// </summary>
        /// <param name="account">Account with changes</param>
        /// <returns>Current account</returns>
        public async Task<Account> UpdateAsync(Account account)
        {
            await Task.Run(() => _context.Accounts.Update(account));
            return account;
        }
    }
}
