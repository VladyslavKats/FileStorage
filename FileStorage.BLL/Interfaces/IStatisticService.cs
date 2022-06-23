using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// Defines basic methods for getting statistic of file storage
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Returns user`s statistic by id of user
        /// </summary>
        /// <param name="userId">User`s id</param>
        /// <returns>Statistic model</returns>
        Task<StatisticModel> GetUserStatisticAsync(string userId);
        /// <summary>
        /// Returns all statistic of every user 
        /// </summary>
        /// <returns>All statistc</returns>
        Task<IEnumerable<StatisticModel>> GetAllStatisticAsync();
        /// <summary>
        /// Returns total statistic of file storage
        /// </summary>
        /// <returns>Total statistic</returns>
        Task<TotalStatisticModel> GetTotalStatisticAsync();
    }
}
