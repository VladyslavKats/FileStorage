using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IStatisticService
    {
        Task<StatisticModel> GetUserStatisticAsync(string userId);

        Task<IEnumerable<StatisticModel>> GetAllStatisticAsync();
    }
}
