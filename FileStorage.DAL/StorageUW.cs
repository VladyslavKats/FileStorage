using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FileStorage.DAL
{
    public class StorageUW : IStorageUW
    {
        private readonly FileStorageContext _context;
        private IRepository<string, Document> _documentRepository;
        private IRepository<string, Account> _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool _disposed;

        public StorageUW(FileStorageContext context, 
            UserManager<User> userManager ,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserManager<User> Users => _userManager;

        public RoleManager<IdentityRole> Roles => _roleManager;

        public IRepository<string, Document> Documents => _documentRepository ??= new DocumentRepository(_context);

        public IRepository<string, Account> Accounts => _accountRepository ??= new AccountRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
