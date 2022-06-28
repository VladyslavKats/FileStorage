using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
           _statisticService = statisticService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatisticModel>>> GetAllAsync()
        {
            try
            {
                var result = await _statisticService.GetAllStatisticAsync();
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<StatisticModel>> GetByUserIdAsync(string id)
        {
            try
            {
                var result = await _statisticService.GetUserStatisticAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        [Route("total")]
        public async Task<ActionResult<TotalStatisticModel>> GetStatisticAsync()
        {
            try
            {
                var result = await _statisticService.GetTotalStatisticAsync();
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
