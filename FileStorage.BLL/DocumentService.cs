using AutoMapper;
using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    public class DocumentService : IDocumentService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;

        public DocumentService(IStorageUW context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<DocumentDto> AddAsync(IFormFile file, string directory , string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);

            if (user == null)
                throw new FileStorageException("Forbidden access to save file for unauthorized user");
            
            string rootPath = Path.Combine(directory, username);
            if(!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            string fileName = file.FileName;
           
            string filePath = Path.Combine(rootPath, file.FileName);

            for (int index = 1; File.Exists(filePath); index++)
            {
                changeFileNameAndPath(fileName, filePath ,index, out fileName, out filePath);
            }
           
            
            using (var stream = new FileStream(filePath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }
            var document = new Document {
                Name = fileName, 
                Size = file.Length, 
                ContentType = file.ContentType , 
                Path = filePath , 
                UserId = user.Id
            };
            await _context.Documents.AddAsync(document);

            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDto>(document);
        }

        private void changeFileNameAndPath(string fileName, string filePath , int index , out string fileNameResult, out string filePathResult)
        {
            var splited = fileName.Split(".");

            if (index == 1)
            {
                splited[0] = $"{splited[0]}({index})";
                fileNameResult = string.Join(".", splited);
            }
            else {
                fileNameResult = Regex.Replace(fileName, @"\(\d+\).", $"({index}).");
            }
           
            filePathResult = changePath(filePath, fileNameResult);
        }

        public async Task<IEnumerable<DocumentDto>> AddRangeAsync(IEnumerable<IFormFile> files, string directory, string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);

            if (user == null)
                throw new FileStorageException("Forbidden access to save file for unauthorized user");
            string rootPath = Path.Combine(directory, username);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            List<Document> result = new List<Document>();
            foreach (var file in files)
            {

                string fileName = file.FileName;

                string filePath = Path.Combine(rootPath, file.FileName);

                for (int index = 1; File.Exists(filePath); index++)
                {
                    changeFileNameAndPath(fileName, filePath, index, out fileName, out filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var document = new Document
                {
                    Name = fileName,
                    Size = file.Length,
                    ContentType = file.ContentType,
                    Path = filePath,
                    UserId = user.Id
                };
                result.Add(await _context.Documents.AddAsync(document));
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(result.ToArray());
        }


        public async Task DeleteAsync(int id)
        {
            var document = await _context.Documents.GetAsync(id);
            if (document == null)
                return;

            File.Delete(document.Path);
            await _context.Documents.DeleteAsync(id);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _context.Documents.GetAllAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<IEnumerable<DocumentDto>> GetAllUserDocumentsAsync(string username)
        {
            var documents = await _context.Documents.GetAllAsync();
            var userDocuments = documents.Where(d => d.User.UserName == username); 
            return _mapper.Map<IEnumerable<DocumentDto>>(userDocuments); 
        }

        public async Task<DocumentDto> UpdateAsync(DocumentDto document)
        {
            string oldPath = document.Path;
            string newPath = changePath(document.Path , document.Name);
            string newName = document.Name;



            for (int index = 1; File.Exists(newPath); index++)
            {
                changeFileNameAndPath(newName, newPath, index, out newName, out newPath);
            }
            File.Move(oldPath, newPath);
            document.Path = newPath;
            document.Name = newName;


            var newDocument = _mapper.Map<Document>(document);

            var result = await _context.Documents.UpdateAsync(newDocument);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDto>(result);
        }

        private string changePath(string oldPath , string newNameFile) {
            var words = oldPath.Split('\\');

            words[^1] = newNameFile;
            string newPath = string.Join('\\', words);
            return newPath;
        }

        public async Task<byte[]> GetDocumentBytesByPathAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }
    }
}
