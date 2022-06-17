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
    internal class DocumentRepositoryTest
    {
       [Test]
        public async Task DocumentRepository_GetAllAsync_ReturnsValues()
        {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var documentRepository = new DocumentRepository(context);

            //Act
            var actual = await documentRepository.GetAllAsync();
            
            //Assert
            Assert.That(actual.Count(), Is.Not.EqualTo(0) , "GetAllAsync method does not return values");
        }


        [Test]
        public async Task DocumentRepository_GetAsync_ReturnsSingleValue() {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var documentRepository = new DocumentRepository(context);


            //Act
            var actual = await documentRepository.GetAsync(1);

            //Assert
            Assert.That(actual, Is.Not.Null, "GetAsync method does not return value");

        }


        [Test]
        public async Task DocumentRepository_AddAsync_AddsValueToBase() {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var documentRepository = new DocumentRepository(context);

            var document = new Document { Name = "test5.txt", Size = 12 };

            //Act
            var actual = await documentRepository.AddAsync(document);
            context.SaveChanges();
            



            //Assert
            Assert.That(context.Documents.Count(), Is.EqualTo(5), "AddAsync method does not add value to database");
            Assert.That(actual.Id,Is.EqualTo(5) , "AddAsync method does not return correct value");

        }


        [Test]
        public async Task DocumentRepository_UpdateAsync_ChangesValue() {
            //Arrange
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var documentRepository = new DocumentRepository(context);

            var expected = new Document {Id = 4 ,  Name = "newName.txt", Size = 25 };

            var before = context.Documents.AsNoTracking().FirstOrDefault(d => d.Id == expected.Id);


            //Act
            var returnValue = await documentRepository.UpdateAsync(expected);
            var actual = context.Documents.FirstOrDefault(d => d.Id == expected.Id);

            //Assert
            Assert.That(actual.Name, Is.EqualTo(expected.Name), "UpdateAsync method does not change name");
            Assert.That(actual.Size, Is.EqualTo(expected.Size), "UpdateAsync method does not change size");
            Assert.That(returnValue , Is.Not.Null);
        }
        [Test]
        public async Task DocumentRepository_DeleteAsync_RemovesValueFromBase() {
            //Arrage
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var documentRepository = new DocumentRepository(context);

            int expectdCount = 3;

            //Act
            await documentRepository.DeleteAsync(1);
            context.SaveChanges();
            var actual = context.Documents.Count();
            //Assert
            Assert.That(actual, Is.EqualTo(expectdCount), "DeleteAsync method does not removes value");

        }

        [Test]
        public async Task DocumentRepository_SaveAsync_WorkCorrect()
        {
            //Arrage
            using var context = new FileStorageContext(UnitTestHelper.GetUnitTestDbOptions());

            var documentRepository = new DocumentRepository(context);

            int expectdCount = 3;

            //Act
            context.Documents.Remove(new Document {Id = 1 });
            await documentRepository.SaveAsync();
            var actual = context.Documents.Count();
            //Assert
            Assert.That(actual, Is.EqualTo(expectdCount), "SaveAsync method does not work correct");
        }
    }
}
