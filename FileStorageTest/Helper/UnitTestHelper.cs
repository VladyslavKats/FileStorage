using FileStorage.DAL.EF;
using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FileStorageTest.Helper
{
    public static class UnitTestHelper
    {

        public static DbContextOptions<FileStorageContext> GetUnitTestDbOptions()
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

        public static void SeedData(FileStorageContext context)
        {
            var user1 = new User { Id = "c7549b86-5e84-4998-a522-3c928e18a7d3", UserName = "test1", Email = "test1@gmail.com", PasswordHash = "test1" };
            var user2 = new User { Id = "9d3aed3f-bab1-448b-8adb-e7ef58953d2e", UserName = "test2", Email = "test2@gmail.com", PasswordHash = "test2" };
            var user3 = new User { Id = "61561a2d-1eb4-4771-a0e3-0a5ab8e74538", UserName = "test3", Email = "test3@gmail.com", PasswordHash = "test3" };
            var user4 = new User { Id = "866f12dd-5533-4877-8412-7b53ad277224", UserName = "test4", Email = "test4@gmail.com", PasswordHash = "test4" };
            context.Users.AddRange(user1, user2, user3, user4);
            context.Accounts.AddRange(
                new Account { Id = user1.Id }, 
                new Account { Id = user2.Id },
                new Account { Id = user3.Id }
            );
            context.Documents.AddRange(new Document[] {
                new Document { Id = "44ee8dca-be90-4b29-b28f-e9a7f41498fe"  , Name = "test1.txt" , Size = 10 , UserId = user1.Id},
                new Document { Id = "22b6f997-7abe-431f-9f80-d48a507d4097"  , Name = "test2.txt" , Size = 20 , UserId = user1.Id},
                new Document { Id ="ab9bf55f-6aff-4ee2-af2c-eb3ccec9d5d3"  , Name = "test3.txt" , Size = 30 , UserId = user1.Id},
                new Document { Id = "6abcb947-91be-4ac1-97a1-d7c16784fd26"  , Name = "test4.txt" , Size = 40 , UserId = user1.Id},
            });
            context.SaveChanges();
        }

    }
}
