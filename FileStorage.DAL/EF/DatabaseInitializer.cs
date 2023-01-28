using FileStorage.DAL.Enums;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileStorage.DAL.EF
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly FileStorageContext _context;
        private const string DEFAULT_PASSWORD = "Admin_123";

        public DatabaseInitializer(FileStorageContext context)
        {
            _context = context;
        }

        public void Initialize(string passwordForAdmin)
        {
            string idForAdminRole = Guid.NewGuid().ToString();
            string idForUserRole = Guid.NewGuid().ToString();
            string idForAdmin = Guid.NewGuid().ToString();
            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id = idForUserRole,
                        Name = Roles.User,
                        NormalizedName = Roles.User.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new IdentityRole
                    {
                        Name = Roles.Admin,
                        NormalizedName = Roles.Admin.ToUpper(),
                        Id = idForAdminRole,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
                });
            }
            if (!_context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();
                var user = new User
                {
                    Id = idForAdmin,
                    Email = "admin@filestorage.com",
                    EmailConfirmed = true,
                    UserName = "administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                };
                user.PasswordHash = passwordHasher
                    .HashPassword(user,passwordForAdmin ?? DEFAULT_PASSWORD);
                _context.Users.Add(user);
            }
            if (!_context.UserRoles.Any())
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = idForAdminRole,
                    UserId = idForAdmin
                });
            }
            _context.SaveChanges();
        }
    }
}
