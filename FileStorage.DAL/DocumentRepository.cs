using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly FileStorageContext _context;

        public DocumentRepository(FileStorageContext context)
        {
            _context = context;
        }


        public async Task<Document> AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            return document;
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() => _context.Documents.Remove(new Document { Id = id }));
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return  await _context.Documents.Include(d => d.User).ToArrayAsync();
        }

        public async Task<IEnumerable<Document>> GetAllAsNoTrackingAsync()
        {
            return await _context.Documents.Include(d => d.User).AsNoTracking().ToArrayAsync();
        }

        public async Task<Document> GetAsNoTrackingAsync(int id)
        {
            return await _context.Documents.Include(d => d.User).AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Document> GetAsync(int id)
        {
            return await _context.Documents.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Document> UpdateAsync(Document document)
        {
            
            await Task.Run(() => _context.Documents.Update(document));
            return document;
        }
    }
}
