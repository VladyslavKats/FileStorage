using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    /// <summary>
    /// Contains all basic methods for manage documents
    /// </summary>
    public interface IDocumentRepository
    {
        /// <summary>
        /// Creates document 
        /// </summary>
        /// <param name="document">Document to create</param>
        /// <returns>Current document</returns>
        Task<Document> AddAsync(Document document);

        /// <summary>
        /// Updates document
        /// </summary>
        /// <param name="document">Document with changes</param>
        /// <returns>Current document</returns>
        Task<Document> UpdateAsync(Document document);

        /// <summary>
        /// Deletes document by id
        /// </summary>
        /// <param name="id">document`s id to delete</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
        /// <summary>
        /// Returns document
        /// </summary>
        /// <param name="id">Document`s id to return</param>
        /// <returns>Document</returns>
        Task<Document> GetAsync(int id);

        /// <summary>
        /// Returns document , but database doesn`t  track it 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document</returns>
        Task<Document> GetAsNoTrackingAsync(int id);

        /// <summary>
        /// Returns all documents
        /// </summary>
        /// <returns>All documents</returns>
        Task<IEnumerable<Document>> GetAllAsync();

        /// <summary>
        /// Returns all documents , but database doesn`t track them
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Document>> GetAllAsNoTrackingAsync();

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
