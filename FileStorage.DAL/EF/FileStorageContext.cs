using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.EF
{
    public class FileStorageContext : IdentityDbContext<User>
    {
        public FileStorageContext(DbContextOptions<FileStorageContext> options) : base(options)
        {
           
           Database.EnsureCreated();

        }

        public DbSet<Document> Documents { get; set; }
       

      

        
    }
}
