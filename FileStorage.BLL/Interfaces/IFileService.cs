using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// Defines methods for managing  files
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves file by path , if file exist rename it
        /// </summary>
        /// <param name="file">file to save</param>
        /// <param name="filePath">path to file</param>
        /// <param name="fileName">file`s name</param>
        void Save(IFormFile file ,ref string filePath , ref string fileName);


        string Rename(string oldPath, string newFileName, out string newFileNameIfOldExist);


        void Delete(string path);

        Task<byte[]> DownloadAsync(string path);
    }
}
