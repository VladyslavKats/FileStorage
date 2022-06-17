using FileStorage.DAL.EF;
using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FileStorageTest.Helper
{
    internal static class UnitTestHelper
    {

        internal static DbContextOptions<FileStorageContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<FileStorageContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new FileStorageContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        internal static void SeedData(FileStorageContext context)
        {
            var user1 = new User {Id = Guid.NewGuid().ToString() ,  UserName = "test1", Email = "test1@gmail.com", PasswordHash = "test1" };

            var user2 = new User {Id = Guid.NewGuid().ToString() ,  UserName = "test2", Email = "test2@gmail.com", PasswordHash = "test2" };
            var user3 = new User {Id = Guid.NewGuid().ToString() ,  UserName = "test3", Email = "test3@gmail.com", PasswordHash = "test3" };
            var user4 = new User {Id = Guid.NewGuid().ToString() ,  UserName = "test4", Email = "test4@gmail.com", PasswordHash = "test4" };

            context.Users.AddRange(user1 , user2 , user3 , user4);
            context.Accounts.AddRange(new Account { Id = user1.Id }, new Account { Id = user2.Id }, new Account { Id = user3.Id });

            context.Documents.AddRange(new Document[] { 
                new Document { Id = 1  , Name = "test1.txt" , Size = 10 , UserId = user1.Id},
                new Document { Id = 2  , Name = "test2.txt" , Size = 20 , UserId = user1.Id},
                new Document { Id = 3  , Name = "test3.txt" , Size = 30 , UserId = user1.Id},
                new Document { Id = 4  , Name = "test4.txt" , Size = 40 , UserId = user1.Id},
            });









            context.SaveChanges();
        }

    }
}
