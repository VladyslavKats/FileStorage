using FileStorage.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IStatisticService
    {
        Task<StatisticModel> GetStatisticByUserAsync(string userId);
       
        Task<IEnumerable<StatisticModel>> GetStatisticAllUsersAsync();
       
        Task<TotalStatisticModel> GetTotalStatisticAsync();
    }
}
