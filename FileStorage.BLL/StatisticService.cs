using AutoMapper;
using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    /// <summary>
    /// Service for getting statistic of file storage
    /// </summary>
    public class StatisticService : IStatisticService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;
        private readonly IOptions<FilesOptions> _options;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Creates instance of service
        /// </summary>
        /// <param name="context">Class for managing file storage</param>
        /// <param name="mapper">Mapper for models</param>
        /// <param name="options">Configuration for files</param>
        public StatisticService(IStorageUW context , IMapper mapper , IOptions<FilesOptions> options)
        {
            _context = context;
            _mapper = mapper;
            _options = options;
           
        }
        /// <summary>
        /// Returns statistic of every user
        /// </summary>
        /// <returns>Users statistc</returns>
        public async Task<IEnumerable<StatisticModel>> GetAllStatisticAsync()
        {
            var accounts = await _context.Accounts.GetAllAsync();

            var result = _mapper.Map<IEnumerable<StatisticModel>>(accounts);

            return result;
        }
        /// <summary>
        /// Return total statistic of storage
        /// </summary>
        /// <returns>Total statistic</returns>
        public async Task<TotalStatisticModel> GetTotalStatisticAsync()
        {
            var accounts = await _context.Accounts.GetAllAsync();
            var totalFiles = accounts.Sum(a => a.Files);
            var totalSpace = accounts.Sum(a => a.UsedSpace);
            var maxSpace = long.Parse(_configuration.GetSection("Files")["TotalSpace"]);
            return new TotalStatisticModel { TotalFiles = totalFiles, TotalUsedSpace = totalSpace , MaxSpace = maxSpace };
        }
        /// <summary>
        /// Returns user`s statistic
        /// </summary>
        /// <param name="userId">User`s id</param>
        /// <returns>User`s statistic</returns>
        /// <exception cref="FileStorageArgumentException"></exception>
        public async Task<StatisticModel> GetUserStatisticAsync(string userId)
        {
            var account = await _context.Accounts.GetByIdAsync(userId);

            if (account == null)
                throw new FileStorageArgumentException("There is not an account!");
            var result = _mapper.Map<StatisticModel>(account);
            return result; 
        }
    }
}
