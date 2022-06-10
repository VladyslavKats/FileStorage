using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FileStorage.PL.Models
{
    public class DocumentAddModel
    {
        public IEnumerable<IFormFile> Files { get; set; }

        public string UserName { get; set; }
    }
}
