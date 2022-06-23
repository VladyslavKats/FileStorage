using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL
{
    /// <summary>
    /// Class for managing file storage
    /// </summary>
    public class StorageUW : IStorageUW
    {
        private readonly FileStorageContext _context;
        private readonly IServiceProvider _provider;
        private  IDocumentRepository _documentRepository;

        private UserManager<User> _userManager;

        private RoleManager<IdentityRole> _roleManager;

        private IAccountRepository _accountRepository;

        /// <summary>
        /// Returns class for managing users
        /// </summary>
        public UserManager<User> UserManager {
            get {
                return _userManager ??= _provider.GetRequiredService<UserManager<User>>();
               
            }
        }
        /// <summary>
        /// Returns class for managing roles
        /// </summary>
        public RoleManager<IdentityRole> RoleManager {
            get {
                return _roleManager ??= _provider.GetRequiredService<RoleManager<IdentityRole>>();
            }
        }
        /// <summary>
        /// Returns class for managing documents
        /// </summary>
        public IDocumentRepository Documents{
            get {
                return _documentRepository ??= new DocumentRepository(_context);
            }
        }

        /// <summary>
        /// Returns class for managing accounts
        /// </summary>
        public IAccountRepository Accounts
        {
            get
            {
                return _accountRepository ??= new AccountRepository(_context);
            }
        }


        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="context">Databse context</param>
        /// <param name="provider">Class for retrieving service objects</param>
        public StorageUW(FileStorageContext context , IServiceProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
