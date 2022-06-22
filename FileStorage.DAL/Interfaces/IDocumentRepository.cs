using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> AddAsync(Document document);

        Task<Document> UpdateAsync(Document document);

        Task DeleteAsync(int id);

        Task<Document> GetAsync(int id);

        Task<Document> GetAsNoTrackingAsync(int id);

        Task<IEnumerable<Document>> GetAllAsync();

        Task<IEnumerable<Document>> GetAllAsNoTrackingAsync();

        Task SaveAsync();
    }
}
