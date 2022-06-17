using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAsync(string userId);

        Task<Account> UpdateAsync(Account account);

        Task DeleteAsync(string id);

        Task<Account> GetByIdAsync(string id);

        Task<IEnumerable<Account>> GetAllAsync();

        Task SaveAsync();
    }
}
