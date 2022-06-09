using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentDto> AddAsync(DocumentDto document, string directory);

        Task<DocumentDto> UpdateAsync(DocumentDto document);

        Task<string> GetUrlAsync(int id);

        Task DeleteAsync(int id);

        Task<IEnumerable<DocumentDto>> GetAllAsync();
    }
}
