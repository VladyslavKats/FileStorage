using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IFileService
    {
        Task UploadAsync(IFormFile file);

        Task DeleteAsync(string fileName);

        Task<byte[]> DownloadAsync(string fileName);
    }
}
