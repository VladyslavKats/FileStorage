using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// Defines basic methods for managing documents in file storage
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Creates file and adds document to database
        /// </summary>
        /// <param name="file">File to create and save</param>
        /// <param name="directory">Basic directory path to files</param>
        /// <param name="username">User`s name</param>
        /// <returns>Created  docuement</returns>
        Task<DocumentDto> AddAsync(IFormFile file, string directory, string username);

        /// <summary>
        /// Creates files and adds documents to database
        /// </summary>
        /// <param name="files">Files to create and save</param>
        /// <param name="directory">>Basic directory path to files</param>
        /// <param name="username">User`s name</param>
        /// <returns></returns>
        Task<IEnumerable<DocumentDto>> AddRangeAsync(IEnumerable<IFormFile> files, string directory, string username);

        /// <summary>
        /// Returns file converted into bytes
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Bytes</returns>
        Task<byte[]> GetDocumentBytesByPathAsync(string path);
        /// <summary>
        /// Edits file name and path
        /// </summary>
        /// <param name="document">Document with changes</param>
        /// <returns>Current document</returns>
        Task<DocumentDto> UpdateAsync(DocumentDto document);
        /// <summary>
        /// Deletes file and removes it in database
        /// </summary>
        /// <param name="id">Document`s id</param>
        /// <param name="username">Owner`s name</param>
        /// <returns></returns>
        Task DeleteAsync(int id , string username);
        /// <summary>
        /// Returns all documents
        /// </summary>
        /// <returns>All documents</returns>
        Task<IEnumerable<DocumentDto>> GetAllAsync();
        /// <summary>
        /// Returns all user`s documents by name of user
        /// </summary>
        /// <param name="username">User`s name</param>
        /// <returns>All documents of user</returns>
        Task<IEnumerable<DocumentDto>> GetAllUserDocumentsAsync(string username);


    }
}
