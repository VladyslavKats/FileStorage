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
    /// <summary>
    /// Class for managing documents
    /// </summary>
    public class DocumentRepository : IDocumentRepository
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly FileStorageContext _context;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="context">Database context</param>
        public DocumentRepository(FileStorageContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds document to database
        /// </summary>
        /// <param name="document">Current document</param>
        /// <returns></returns>
        public async Task<Document> AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            return document;
        }
        /// <summary>
        /// Deletes document
        /// </summary>
        /// <param name="id">Document`s id to delete</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await Task.Run(() => _context.Documents.Remove(new Document { Id = id }));
        }
        /// <summary>
        /// Returns all documents
        /// </summary>
        /// <returns>All documents</returns>
        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return  await _context.Documents.Include(d => d.User).ToArrayAsync();
        }
        /// <summary>
        /// Returns all documents , but database does not track them
        /// </summary>
        /// <returns>All documents</returns>
        public async Task<IEnumerable<Document>> GetAllAsNoTrackingAsync()
        {
            return await _context.Documents.Include(d => d.User).AsNoTracking().ToArrayAsync();
        }
        /// <summary>
        /// Returns document by id , but database does not track it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document</returns>
        public async Task<Document> GetAsNoTrackingAsync(int id)
        {
            return await _context.Documents.Include(d => d.User).AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }
        /// <summary>
        /// Returns document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document</returns>
        public async Task<Document> GetAsync(int id)
        {
            return await _context.Documents.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        }
        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates document
        /// </summary>
        /// <param name="document">Document with changes</param>
        /// <returns>Current document</returns>
        public async Task<Document> UpdateAsync(Document document)
        {
            
            await Task.Run(() => _context.Documents.Update(document));
            return document;
        }
    }
}
