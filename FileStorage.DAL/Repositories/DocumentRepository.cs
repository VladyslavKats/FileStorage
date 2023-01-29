using FileStorage.DAL.EF;
using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories
{
    public class DocumentRepository : GenericRepository<string, Document>
    {
        public DocumentRepository(FileStorageContext context) : base(context)
        {
        }
    }
}
