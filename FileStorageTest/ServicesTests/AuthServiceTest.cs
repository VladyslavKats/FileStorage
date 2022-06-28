using FileStorage.BLL;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageTest.ServicesTests
{
    [TestFixture]
    public class AuthServiceTest
    {
        [Test]
        public async Task AuthService_CheckUserNameAsync_ShouldReturnTrue()
        {
            //Arrange
            var unitOfWork = new Mock<IStorageUW>();
            var user = getUsers().First();

            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync(user);
            unitOfWork.Setup(u => u.UserManager).Returns(mockUserManager.Object);
            
            var authService = new AuthService(unitOfWork.Object, null, null);
            //Act
            var actual = await authService.CheckUserNameAsync(user.UserName);

            //Assert
            Assert.That(actual, Is.EqualTo(true), "CheckUserNameAsync method does not return true");
        }


        [Test]
        public async Task AuthService_CheckUserNameAsync_ShouldReturnFalse()
        {
            //Arrange
            var unitOfWork = new Mock<IStorageUW>();
            var user = getUsers().First();

            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            mockUserManager.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync((User)null);
            unitOfWork.Setup(u => u.UserManager).Returns(mockUserManager.Object);

            var authService = new AuthService(unitOfWork.Object, null, null);
            //Act
            var actual = await authService.CheckUserNameAsync(user.UserName);

            //Assert
            Assert.That(actual, Is.EqualTo(false), "CheckUserNameAsync method does not return false");
        }







        private List<User> getUsers()
        {
            return new List<User>() { 
                new User{ Id = Guid.NewGuid().ToString() , UserName  = "user1" },
                new User{Id = Guid.NewGuid().ToString() , UserName  = "user2" },
                new User{Id = Guid.NewGuid().ToString() , UserName  = "user3" }
            };
        }
    }
}
