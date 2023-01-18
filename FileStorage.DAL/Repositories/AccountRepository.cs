using FileStorage.DAL.EF;
using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories
{
    public class AccountRepository : GenericRepository<string, Account>
    {
        public AccountRepository(FileStorageContext context) : base(context)
        {
        }
    }
}
