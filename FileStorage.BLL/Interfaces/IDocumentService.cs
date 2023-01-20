using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IDocumentService
    {
        Task AddAsync(IFormFile file , string userId);

        Task AddAsync(IEnumerable<IFormFile> files, string userId);

        Task<byte[]> DownloadAsync(string id);

        Task DeleteAsync(string id);

        Task<IEnumerable<DocumentDto>> GetAllAsync();

        Task<IEnumerable<DocumentDto>> GetAllByUserAsync(string userId);

        Task<DocumentDto> GetDetailsAsync(string id);
    }
}
