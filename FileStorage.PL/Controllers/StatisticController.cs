using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
           _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatisticModel>>> GetAllAsync()
        {
            try
            {
                var result = await _statisticService.GetAllStatisticAsync();
                return Ok(result);
            }
            catch (FileStorageException ex)
            {

                return BadRequest(ex.Message);
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
            catch (FileStorageException ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
