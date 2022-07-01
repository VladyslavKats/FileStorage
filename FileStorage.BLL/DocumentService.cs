using AutoMapper;
using FileStorage.BLL.Common;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    /// <summary>
    /// Service for managing documents
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;
        private readonly IOptions<FilesOptions> _options;
        private readonly IFileService _fileService;

        /// <summary>
        /// Creates instance of service
        /// </summary>
        /// <param name="context">Class for managing file storage</param>
        /// <param name="mapper">Mapper for models</param>
        /// <param name="options">Options for files settings</param>
        public DocumentService(IStorageUW context , IMapper mapper , IOptions<FilesOptions> options , IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
           _options = options;
            _fileService = fileService;
        }

        /// <summary>
        /// Creates and adds file to database
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="directory">Path to directory with files</param>
        /// <param name="username">User`s name</param>
        /// <returns>Current file</returns>
        /// <exception cref="FileStorageAuthenticateException">Occurs when user have not access for it</exception>
        /// <exception cref="FileStorageException">Occurs when data is incorrect</exception>
        public async Task<DocumentDto> AddAsync(IFormFile file, string directory , string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);

            if (user == null)
                throw new FileStorageAuthenticateException("Forbidden access to save file for unauthorized user");
            
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

      
        /// <summary>
        /// Creates and adds files to database
        /// </summary>
        /// <param name="files">Files to create</param>
        /// <param name="directory">Path to directory with files</param>
        /// <param name="username">User`s name</param>
        /// <returns>Current files</returns>
        /// <exception cref="FileStorageAuthenticateException">Occurs when user does not exist</exception>
        /// <exception cref="FileStorageException">Occurs when data is incorrect</exception>
        public async Task<IEnumerable<DocumentDto>> AddRangeAsync(IEnumerable<IFormFile> files, string directory, string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);

            if (user == null)
                throw new FileStorageAuthenticateException("Forbidden access to save file for unauthorized user");


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

        /// <summary>
        /// Deletes file 
        /// </summary>
        /// <param name="id">Ducument`s id</param>
        /// <param name="username">User`s name</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id , string username)
        {
            var user = await _context.UserManager.FindByNameAsync(username);
            if (user == null)
                throw new FileStorageAuthenticateException();

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


        /// <summary>
        /// Returns all documents
        /// </summary>
        /// <returns>All documents</returns>
        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _context.Documents.GetAllAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        /// <summary>
        /// Returns all user`s documents
        /// </summary>
        /// <param name="username">User`s name</param>
        /// <returns>User`s documents</returns>
        public async Task<IEnumerable<DocumentDto>> GetAllUserDocumentsAsync(string username)
        {
            var documents = await _context.Documents.GetAllAsync();
            var userDocuments = documents.Where(d => d.User.UserName == username); 
            return _mapper.Map<IEnumerable<DocumentDto>>(userDocuments); 
        }


        /// <summary>
        /// Changes file data
        /// </summary>
        /// <param name="document">Document with changes</param>
        /// <returns>Current document</returns>
        public async Task<DocumentDto> RenameAsync(DocumentDto document)
        {
            
            document.Path = _fileService.Rename(document.Path ,document.Name , out string newNameIfExist);
            document.Name = newNameIfExist;

            var savedDocument = _mapper.Map<Document>(document);

            var result = await _context.Documents.UpdateAsync(savedDocument);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDto>(result);
        }

       
        /// <summary>
        /// Returns document in bytes
        /// </summary>
        /// <param name="path">Path to document</param>
        /// <returns>Document is converted into bytes</returns>
        public async Task<byte[]> DownloadAsync(string path)
        {
            return await _fileService.DownloadAsync(path);
        }
    }
}
