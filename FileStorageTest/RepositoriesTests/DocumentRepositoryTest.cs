using FileStorage.DAL;
using FileStorage.DAL.EF;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories;
using FileStorageTest.Comparers;
using FileStorageTest.Helper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageTest.RepositoriesTests
{
    [TestFixture]
     public class DocumentRepositoryTest
     {
        private  DocumentRepository _repository;
        private FileStorageContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());
            _repository = new DocumentRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturn_Values()
        {
            //Act
            var actual = await _repository.GetAllAsync();
            //Assert
            Assert.That(actual.Count(), Is.EqualTo(4), "GetAllAsync method does not return values");
        }

        [Test]
        public async Task GetAsync_ShouldReturns_SingleValue()
        {
            //Arrange
            var expected = new Document
            {
                Id = "44ee8dca-be90-4b29-b28f-e9a7f41498fe",
                Name = "test1.txt",
                Size = 10,
                UserId = "c7549b86-5e84-4998-a522-3c928e18a7d3"
            };
            //Act
            var actual = await _repository.GetAsync(expected.Id);
            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new DocumentComparer()),
                "GetAsync method does not return value");
        }

        [Test]
        public async Task AddAsync_ShouldAdd_DocumentToDatabase()
        {
            //Arrange
            var document = new Document { 
                Name = "test5.txt", 
                Size = 12 ,
                ContentType = "text/plain" , 
                UserId = "c7549b86-5e84-4998-a522-3c928e18a7d3" 
            };
            //Act
            var actual = await _repository.AddAsync(document);
            _context.SaveChanges();
            //Assert
            var expected = await _context.Documents
                .FirstOrDefaultAsync(d => d.Name == document.Name);
            Assert.That(expected, Is.Not.Null, 
                "AddAsync method does not add document to database");
        }


        //[Test]
        //public async Task DocumentRepository_UpdateAsync_ChangesValue()
        //{
        //    //Arrange
        //    using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

        //    var documentRepository = new DocumentRepository(context);

        //    var expected = new Document { Id = 4, Name = "newName.txt", Size = 25 };

        //    var before = context.Documents.AsNoTracking().FirstOrDefault(d => d.Id == expected.Id);


        //    //Act
        //    var returnValue = await documentRepository.UpdateAsync(expected);
        //    var actual = context.Documents.FirstOrDefault(d => d.Id == expected.Id);

        //    //Assert
        //    Assert.That(actual.Name, Is.EqualTo(expected.Name), "UpdateAsync method does not change name");
        //    Assert.That(actual.Size, Is.EqualTo(expected.Size), "UpdateAsync method does not change size");
        //    Assert.That(returnValue, Is.Not.Null);
        //}
        //[Test]
        //public async Task DocumentRepository_DeleteAsync_RemovesValueFromDatabase()
        //{
        //    //Arrage
        //    using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

        //    var documentRepository = new DocumentRepository(context);

        //    int expectdCount = 3;

        //    //Act
        //    await documentRepository.DeleteAsync(1);
        //    context.SaveChanges();
        //    var actual = context.Documents.Count();
        //    //Assert
        //    Assert.That(actual, Is.EqualTo(expectdCount), "DeleteAsync method does not removes value");

        //}
    }
}
