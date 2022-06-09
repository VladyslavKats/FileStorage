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
            var user = new User {Id = Guid.NewGuid().ToString() ,  UserName = "test", Email = "test@gmail.com", PasswordHash = "test" };
            context.Users.Add(user);


            context.Documents.AddRange(new Document[] { 
                new Document { Id = 1  , Name = "test1.txt" , Size = 10 , UserId = user.Id},
                new Document { Id = 2  , Name = "test2.txt" , Size = 20 , UserId = user.Id},
                new Document { Id = 3  , Name = "test3.txt" , Size = 30 , UserId = user.Id},
                new Document { Id = 4  , Name = "test4.txt" , Size = 40 , UserId = user.Id},
            });
            context.SaveChanges();
        }

    }
}
