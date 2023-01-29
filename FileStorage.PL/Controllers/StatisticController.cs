using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Enums;
using FileStorage.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        private readonly CurrentUser _currentUser;

        public StatisticController(IStatisticService statisticService, CurrentUser currentUser)
        {
            _statisticService = statisticService;
            _currentUser = currentUser;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<StatisticModel>>> GetAllAsync()
        {
            var result = await _statisticService.GetStatisticAllUsersAsync();
            return Ok(result);
        }

        
        [HttpGet]
        public async Task<ActionResult<StatisticModel>> GetByUserIdAsync()
        {
            var result = await _statisticService.GetStatisticByUserAsync(_currentUser.GetId());
            return Ok(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("total")]
        public async Task<ActionResult<TotalStatisticModel>> GetStatisticAsync()
        {
            var result = await _statisticService.GetTotalStatisticAsync();
            return Ok(result);
        }

    }
}
