using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IFileService
    {
        Task<FileDetails> UploadAsync(IFormFile file , string name);

        Task DeleteAsync(string fileName);

        Task<byte[]> DownloadAsync(string fileName);
    }
}
