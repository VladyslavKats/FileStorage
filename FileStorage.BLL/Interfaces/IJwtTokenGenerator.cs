using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateAsync(string userId , IEnumerable<string> roles);
    }
}
