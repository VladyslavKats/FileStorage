using FileStorage.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    public class FileService : IFileService
    {

        public void Save(IFormFile file , ref string filePath, ref string fileName)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Path or name is incorrect");


            if (File.Exists(filePath))
                changePathAndNameFileIfExist(ref filePath , ref fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        private void changePathAndNameFileIfExist(ref string filePath, ref string fileName)
        {
            string newName = string.Empty;
            int index = 1;
            do
            {
                
                var splitedName = fileName.Split(".");
                var name = string.Join("." , splitedName.Take(splitedName.Length - 1));
                var extension = splitedName[^1];
                newName = $"{name}({index}).{extension}";
                filePath = changePath(filePath , newName);
                index++;

            } while (File.Exists(filePath));
            fileName = newName;
        }

        private string changePath(string oldPath, string newName)
        {
            var words = oldPath.Split('\\');

            words[^1] = newName;
            string newPath = string.Join('\\', words);
            return newPath;
        }

        public string Rename(string oldPath, string newFileName , out string newFileNameIfOldExist)
        {
            
            string newPath = changePath(oldPath, newFileName);

            if (File.Exists(newPath))
                changePathAndNameFileIfExist(ref newPath , ref newFileName);

            File.Move(oldPath, newPath);
            newFileNameIfOldExist = newFileName;
            return newPath;
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public async Task<byte[]> DownloadAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }
    }
}
