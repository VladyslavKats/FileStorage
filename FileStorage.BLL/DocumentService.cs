using AutoMapper;
using FileStorage.BLL.Exceptions;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    
    public class DocumentService : IDocumentService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;
        private readonly IOptions<FilesOptions> _options;
        private readonly IFileService _fileService;

        
        public DocumentService(IStorageUW context , IMapper mapper , IOptions<FilesOptions> options , IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
           _options = options;
            _fileService = fileService;
        }

        
        public async Task<DocumentDto> AddAsync(IFormFile file, string directory , string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);

            if (user == null)
                throw new AuthenticateException("Forbidden access to save file for unauthorized user");
            
            var account = await _context.Accounts.GetByIdAsync(user.Id);

            long maxSize = _options.Value.MaxSizeSpace;

            long futureMemory = file.Length + account.UsedSpace;

            if (maxSize < futureMemory)
            {
                throw new FileStorageException("There is not memory!");
            }

            string rootPath = Path.Combine(directory, username);
            if(!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            string fileName = file.FileName;
           
            string filePath = Path.Combine(rootPath, file.FileName);


            _fileService.Save(file ,ref filePath , ref fileName);


            var document = new Document {
                Name = fileName, 
                Size = file.Length, 
                ContentType = file.ContentType , 
                Path = filePath , 
                UserId = user.Id
            };
            await _context.Documents.AddAsync(document);

           
            account.Files += 1;
            account.UsedSpace = futureMemory;
            await _context.Accounts.UpdateAsync(account);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDto>(document);
        }

      
        
        public async Task<IEnumerable<DocumentDto>> AddRangeAsync(IEnumerable<IFormFile> files, string directory, string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);

            if (user == null)
                throw new AuthenticateException("Forbidden access to save file for unauthorized user");


            var account = await _context.Accounts.GetByIdAsync(user.Id);

            long maxSize = _options.Value.MaxSizeSpace;

            long futureMemory = account.UsedSpace + files.Sum(f => f.Length);

            if (maxSize < futureMemory)
            {
                throw new FileStorageException("There is not memory!");
            }

            string rootPath = Path.Combine(directory, username);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            List<Document> result = new List<Document>();
            foreach (var file in files)
            {

                string fileName = file.FileName;

                string filePath = Path.Combine(rootPath, file.FileName);


                _fileService.Save(file ,ref filePath ,ref fileName);

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
            account.UsedSpace = futureMemory;
            account.Files += files.Count();
            await _context.SaveChangesAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(result.ToArray());
        }

        
        public async Task DeleteAsync(int id , string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);
            if (user == null)
                throw new AuthenticateException();

            var account = await _context.Accounts.GetByIdAsync(user.Id);


            var document = await _context.Documents.GetAsNoTrackingAsync(id);
            if (document == null)
                throw new ArgumentNullException();

            _fileService.Delete(document.Path);
            account.Files -= 1;

            account.UsedSpace -= document.Size;

            await _context.Documents.DeleteAsync(id);
            await _context.Accounts.UpdateAsync(account);
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

        public async Task<DocumentDto> RenameAsync(DocumentDto document)
        {
            
            document.Path = _fileService.Rename(document.Path ,document.Name , out string newNameIfExist);
            document.Name = newNameIfExist;

            var savedDocument = _mapper.Map<Document>(document);

            var result = await _context.Documents.UpdateAsync(savedDocument);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDto>(result);
        }

       
        public async Task<byte[]> DownloadAsync(string path)
        {
            return await _fileService.DownloadAsync(path);
        }
    }
}
