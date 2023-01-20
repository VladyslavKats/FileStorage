using AutoMapper;
using FileStorage.BLL.Exceptions;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    public class StatisticService : IStatisticService
    {
        private readonly IStorageUW _context;
        private readonly IOptions<FilesOptions> _options;
        
        public StatisticService(IStorageUW context , IOptions<FilesOptions> options)
        {
            _context = context;
            _options = options;
           
        }
        
        public async Task<IEnumerable<StatisticModel>> GetStatisticAllUsersAsync()
        {
            var accounts = await _context.Accounts.GetAllAsync();
            var result = accounts.Select(account => new StatisticModel {
                Files = account.Files,
                MaxSpace = _options.Value.MaxSizeSpace,
                UsedSpace = account.UsedSpace,
                UserName = account.User.UserName
            });
            return result;
        }
 
        public async Task<TotalStatisticModel> GetTotalStatisticAsync()
        {
            var accounts = await _context.Accounts.GetAllAsync();
            var totalFiles = accounts.Sum(a => a.Files);
            var totalSpace = accounts.Sum(a => a.UsedSpace);
            var maxSpace = _options.Value.TotalSpace;
            return new TotalStatisticModel { TotalFiles = totalFiles, TotalUsedSpace = totalSpace , MaxSpace = maxSpace };
        }

        public async Task<StatisticModel> GetStatisticByUserAsync(string userId)
        {
            var account = await _context.Accounts.GetAsync(userId);
            if (account == null)
            {
                throw new EntityDoesNotExistException("Account with such id does not exist");
            }
            return new StatisticModel { 
                Files = account.Files ,
                MaxSpace = _options.Value.MaxSizeSpace,
                UsedSpace = account.UsedSpace , 
                UserName = account.User.UserName 
            };
        }
    }
}
