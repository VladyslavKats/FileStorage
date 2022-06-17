using AutoMapper;
using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    public class StatisticService : IStatisticService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;

        public StatisticService(IStorageUW context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatisticModel>> GetAllStatisticAsync()
        {
            var accounts = await _context.Accounts.GetAllAsync();

            var result = _mapper.Map<IEnumerable<StatisticModel>>(accounts);

            return result;
        }

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
