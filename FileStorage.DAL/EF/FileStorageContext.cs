using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
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

        public DbSet<Account> Accounts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID_USER = Guid.NewGuid().ToString();
            string ROLE_ID_ADMIN = Guid.NewGuid().ToString();



            builder.Entity<Account>()
                .Property(a => a.Files)
                .HasDefaultValue(0);


            builder.Entity<Account>()
                .Property(a => a.UsedSpace)
                .HasDefaultValue(0);


            builder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Account>(a => a.Id);




            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID_ADMIN,
                ConcurrencyStamp = ROLE_ID_ADMIN
            });

            //create user
            var admin = new User
            {
                Id = ADMIN_ID,
                Email = "kac9661@gmail.com",
                EmailConfirmed = false,
                UserName = "admin",
                NormalizedUserName = "ADMIN"
              
            };

            //set user password
            PasswordHasher<User> ph = new PasswordHasher<User>();
            admin.PasswordHash = ph.HashPassword(admin, "Admin_123");

            //seed user
            builder.Entity<User>().HasData(admin);

            builder.Entity<Account>().HasData(new Account {Id = ADMIN_ID });

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID_ADMIN,
                UserId = ADMIN_ID
            });




            base.OnModelCreating(builder);
        }

       

    }
}
