using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FileStorage.DAL.EF
{
    public class FileStorageContext : IdentityDbContext<User>
    {
        public FileStorageContext(DbContextOptions<FileStorageContext> options) : base(options)
        {
           Database.EnsureCreated();
        }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(FileStorageContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
