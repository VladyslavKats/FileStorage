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
    public class StorageUW : IStorageUW
    {
        private readonly FileStorageContext _context;
        private readonly IServiceProvider _provider;
        private  IDocumentRepository _documentRepository;

        private UserManager<User> _userManager;

        private RoleManager<IdentityRole> _roleManager;



        public UserManager<User> UserManager {
            get {
                return _userManager ??= _provider.GetRequiredService<UserManager<User>>();
            }
        }

        public RoleManager<IdentityRole> RoleManager {
            get {
                return _roleManager ??= _provider.GetRequiredService<RoleManager<IdentityRole>>();
            }
        }

        public IDocumentRepository Documents{
            get {
                return _documentRepository ??= new DocumentRepository(_context);
            }
        }



        public StorageUW(FileStorageContext context , IServiceProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
