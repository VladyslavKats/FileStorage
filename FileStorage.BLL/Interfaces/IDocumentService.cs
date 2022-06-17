using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentDto> AddAsync(IFormFile file, string directory, string username);

        Task<IEnumerable<DocumentDto>> AddRangeAsync(IEnumerable<IFormFile> files, string directory, string username);

        Task<byte[]> GetDocumentBytesByPathAsync(string path);

        Task<DocumentDto> UpdateAsync(DocumentDto document);

        Task DeleteAsync(int id , string username);

        Task<IEnumerable<DocumentDto>> GetAllAsync();

        Task<IEnumerable<DocumentDto>> GetAllUserDocumentsAsync(string username);


    }
}
