using FileStorage.DAL;
using FileStorage.DAL.EF;
using FileStorage.DAL.Models;
using FileStorageTest.Helper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageTest
{
    [TestFixture]
    public class AccountRepositoryTest
    {
        [Test]
        public async Task AccoutRepository_CreateAsync_CreatesValue()
        {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var accountRepository = new AccountRepository(context);

            var user = context.Users.FirstOrDefault(u => u.UserName == "test4");
            int initialCount = context.Accounts.Count();
            int expected = 5;
            //act
            await accountRepository.CreateAsync(user.Id);
            context.SaveChanges();
            int actual = context.Accounts.Count();
            //Assert
            Assert.That(actual , Is.EqualTo(expected) , "CreateAsync method does not work!");
        }

        [Test]
        public async Task AccoutRepository_UpdateAsync_UpdatesValue() {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var accountRepository = new AccountRepository(context);
            var user = context.Users.FirstOrDefault(u => u.UserName == "test3");
            var account = context.Accounts.FirstOrDefault(a => a.Id == user.Id);
            int expectedFiles = 10;
            int expectedUsedSpace = 100;
            account.UsedSpace = expectedUsedSpace;
            account.Files = expectedFiles;

            //Act
            var actual1 = await accountRepository.UpdateAsync(account);
            context.SaveChanges();
            var actual2 = context.Accounts.AsNoTracking().FirstOrDefault(a => a.Id == account.Id);
            //Assert
            Assert.That(actual1.Files, Is.EqualTo(expectedFiles), "UpdateAsync method does not work!");
            Assert.That(actual1.UsedSpace, Is.EqualTo(expectedUsedSpace), "UpdateAsync method does not work!");
            Assert.That(actual2.Files, Is.EqualTo(expectedFiles), "UpdateAsync method does not work!");
            Assert.That(actual2.UsedSpace, Is.EqualTo(expectedUsedSpace), "UpdateAsync method does not work!");
        }

        [Test]
        public async Task AccoutRepository_GetByIdAsync_ReturnsSingleValue()
        {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var accountRepository = new AccountRepository(context);

            var user = context.Users.FirstOrDefault(u => u.UserName == "test3");
            //Act
            var actual = await accountRepository.GetByIdAsync(user.Id);

            //Assert
            Assert.That(actual , Is.Not.Null , "GetByIdAsync method does not return value!");
        }

        [Test]
        public async Task AccoutRepository_GetAllAsync_ReturnsValue()
        {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var accountRepository = new AccountRepository(context);

            int expectedCount = 4;
            //Act
            var actual = await accountRepository.GetAllAsync();

            //Assert
            Assert.That(actual.Count(),Is.EqualTo(expectedCount), "GetAllAsync method does not return values!");
        }

        [Test]
        public async Task AccoutRepository_DeleteAsync_RemovesValue()
        {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var accountRepository = new AccountRepository(context);
            var user = context.Users.FirstOrDefault(u => u.UserName == "test3");
            int expectedCount = 3;
            //Act
            await accountRepository.DeleteAsync(user.Id);
            context.SaveChanges();
            var actualCount = context.Accounts.Count();
            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "DeleteAsync method does not work!");
        }

        [Test]
        public async Task AccoutRepository_SaveAsync_WorkCorrect()
        {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var accountRepository = new AccountRepository(context);
            var user = context.Users.FirstOrDefault(u => u.UserName == "test3");
            int expectedCount = 3;
            //Act
            context.Accounts.Remove(new Account {Id= user.Id });
            await accountRepository.SaveAsync();
            var actualCount = context.Accounts.Count();
            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "SaveAsync method does not work!");
        }
    }
}
