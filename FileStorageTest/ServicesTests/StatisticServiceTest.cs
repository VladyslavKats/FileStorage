using AutoMapper;
using FileStorage.BLL;
using FileStorage.BLL.Common;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using FileStorageTest.Comparers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
    public class StatisticServiceTest
    {
        [Test]
        public async Task StatisticService_GetAllStatisticAsync_ShouldReturnUsersStatistic()
        {
            //Arrange
            var unitOfWork = new Mock<IStorageUW>();
            unitOfWork.Setup(u => u.Accounts.GetAllAsync()).ReturnsAsync(getAllAccounts().AsEnumerable());
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig(new FilesOptions { MaxSizeSpace = 10000 })));
            var mapper = config.CreateMapper();

            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            mockUserManager.Setup(u => u.IsInRoleAsync(It.IsAny<User>(), "Admin")).ReturnsAsync(false);
            unitOfWork.Setup(u => u.UserManager).Returns(mockUserManager.Object);
            var statisticService = new StatisticService(unitOfWork.Object , mapper , null );
            var expected = mapper.Map<IEnumerable<StatisticModel>>(getAllAccounts().AsEnumerable());
            //Act

            var actual = await statisticService.GetAllStatisticAsync();

            //Assert
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()), "GetAllStatisticAsync method does not return value");
        }


        [Test]
        public async Task StatisticService_GetTotalStatisticAsync_ShouldReturnValue()
        {
            //Arrange
            var unitOfWork = new Mock<IStorageUW>();
            unitOfWork.Setup(u => u.Accounts.GetAllAsync()).ReturnsAsync(getAllAccounts().AsEnumerable());
            IOptions<FilesOptions> options = Options.Create<FilesOptions>(new FilesOptions { TotalSpace = 100000  });
           
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig(new FilesOptions { MaxSizeSpace = 10000  , TotalSpace = 100000})));
            var mapper = config.CreateMapper();
           
            var statisticService = new StatisticService(unitOfWork.Object, mapper, options);
            var expected = new TotalStatisticModel { MaxSpace = 100000, TotalFiles = 57, TotalUsedSpace = 87000 };
            //Act
            var actual = await statisticService.GetTotalStatisticAsync();

            //Assert
            Assert.That(actual.TotalFiles , Is.EqualTo(expected.TotalFiles) , "GetTotalStatisticAsync method returns incorrect files number");
            Assert.That(actual.TotalUsedSpace , Is.EqualTo(expected.TotalUsedSpace) , "GetTotalStatisticAsync method returns incorrect used space number");
        }


        [Test]
        public async Task StatisticService_GetUserStatisticAsync_ShouldReturnValue()
        {
            //Arrange
            var unitOfWork = new Mock<IStorageUW>();
            var testAccount = getAllAccounts().First();
            unitOfWork.Setup(u => u.Accounts.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(testAccount);
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig(new FilesOptions { MaxSizeSpace = 10000, TotalSpace = 100000 })));
            var mapper = config.CreateMapper();
            var statisticService = new StatisticService(unitOfWork.Object, mapper, null);
            //Act
            var actual = await statisticService.GetUserStatisticAsync(testAccount.Id);

            //Assert

            Assert.That(actual.Files, Is.EqualTo(testAccount.Files), "GetUserStatisticAsync method returns incorrect files number");
            Assert.That(actual.UsedSpace, Is.EqualTo(testAccount.UsedSpace), "GetUserStatisticAsync method returns incorrect used space number");
            Assert.That(actual.UserName, Is.EqualTo(testAccount.User.UserName), "GetUserStatisticAsync method returns incorrect user name");
            Assert.That(actual.MaxSpace, Is.EqualTo(10000), "GetUserStatisticAsync method returns incorrect max space number");


        }







        private List<Account> getAllAccounts()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var id3 = Guid.NewGuid().ToString();
            var id4 = Guid.NewGuid().ToString();
            return new List<Account>() { 
                new Account { Id =id1 , Files = 10 , UsedSpace = 12000  , User = new User { UserName = "test" } },
                new Account { Id =id2 , Files = 12 , UsedSpace = 15000 },
                new Account { Id =id3 , Files = 15 , UsedSpace = 20000 },
                new Account { Id =id4 , Files = 20 , UsedSpace = 40000 }
            };
        }


       



    }
}
