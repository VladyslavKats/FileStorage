using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FileStorage.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobClient;
        private readonly AzureStorageOptions _options;

        public FileService(BlobServiceClient blobClient, IOptions<AzureStorageOptions> options)
        {
            _blobClient = blobClient;
            _options = options.Value;
        }

        public async Task DeleteAsync(string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(_options.UserFilesContainer);
            var blob = container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }

        public async Task<byte[]> DownloadAsync(string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(_options.UserFilesContainer);
            var blob = container.GetBlobClient(fileName);
            var content = await blob.DownloadContentAsync();
            return content.Value.Content.ToArray();
        }

        public async Task UploadAsync(IFormFile file)
        {
            var container = _blobClient.GetBlobContainerClient(_options.UserFilesContainer);
            var blob = container.GetBlobClient(file.Name);
            await blob.UploadAsync(file.OpenReadStream());
        }
    }
}
