using AutoMapper;
using FileStorage.BLL.Exceptions;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    
    public class DocumentService : IDocumentService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DocumentService(IStorageUW context , IMapper mapper , IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task AddAsync(IFormFile file, string userId)
        {
            var userExist = await _context.Users.FindByIdAsync(userId);
            if (userExist == null)
            {
                throw new EntityDoesNotExistException("User with such id does not exist");
            }
            await AddOrUpdateDocumentAsync(userId , file);
            await _context.CommitAsync();
        }

        public async Task AddAsync(IEnumerable<IFormFile> files, string userId)
        {
            var userExist = await _context.Users.FindByIdAsync(userId);
            if (userExist == null)
            {
                throw new EntityDoesNotExistException("User with such id does not exist");
            }
            foreach (var file in files)
            {
                await AddOrUpdateDocumentAsync(userId, file);
            }
            await _context.CommitAsync();
        }

        private async Task AddOrUpdateDocumentAsync(string userId, IFormFile file)
        {
            var result = await _fileService
                                .UploadAsync(file, GetName(file.FileName, userId));
            var documentExist = await _context.Documents.GetAsync(d =>
                d.UserId == userId &&
                d.Name == result.Name
            );
            if (documentExist != null)
            {
                documentExist.Size = result.Size;
                await _context.Documents.UpdateAsync(documentExist);
            }
            else
            {
                var document = new Document
                {
                    UserId = userId,
                    Name = result.Name,
                    ContentType = result.ContentType,
                    Size = result.Size
                };
                await _context.Documents.AddAsync(document);
            }
        }

        private string GetName(string fileName , string userId)
        {
            return $"{userId}-{fileName}";
        }

        public async Task DeleteAsync(string id,string userId)
        {
            var document = await _context.Documents.GetAsync(id);
            if (document == null)
            {
                throw new EntityDoesNotExistException("Document with such id does not exist");
            }
            await _fileService.DeleteAsync(GetName(document.Name , userId));
            await _context.Documents.DeleteAsync(document);
            await _context.CommitAsync();
        }

        public async Task<DownloadModel> DownloadAsync(string id , string userId)
        {
            var document = await _context.Documents.GetAsync(id);
            if (document == null)
            {
                throw new EntityDoesNotExistException("Document with such id does not exist");
            }
            var file =  await _fileService.DownloadAsync(GetName(document.Name, userId));
            return new DownloadModel
            {
                 Name = document.Name,
                 ContentType = document.ContentType,
                 Document = file
            };
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _context.Documents.GetAllAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<IEnumerable<DocumentDto>> GetAllByUserAsync(string userId)
        {
            var userExist = await _context.Users.FindByIdAsync(userId);
            if (userExist == null)
            {
                throw new EntityDoesNotExistException("User with such id does not exist");
            }
            var documents = await _context.Documents.GetAllAsync();
            var userDocuments = documents.Where(d => d.UserId == userId);
            return _mapper.Map<IEnumerable<DocumentDto>>(userDocuments);
        }

        public async Task<DocumentDto> GetDetailsAsync(string id)
        {
            var document = await _context.Documents.GetAsync(id);
            if (document == null)
            {
                throw new EntityDoesNotExistException("Document with such id does not exist");
            }
            return _mapper.Map<DocumentDto>(document);
        }
    }
}
