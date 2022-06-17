using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FileStorageContext _context;

        public AccountRepository(FileStorageContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string userId)
        {
            await Task.Run(() => _context.Accounts.Add(new Account { Id = userId , Files = 0 , UsedSpace = 0}));
        }

        public async Task DeleteAsync(string id)
        {
           await Task.Run(() => _context.Accounts.Remove(new Account { Id = id })); 
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(a => a.User).ToArrayAsync();
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await _context.Accounts.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            await Task.Run(() => _context.Accounts.Update(account));
            return account;
        }
    }
}
