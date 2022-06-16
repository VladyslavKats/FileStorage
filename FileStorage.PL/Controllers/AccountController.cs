using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<bool>> CheckUserNameAsync(string username)
        {
            try
            {
                bool result = await _authService.CheckUserNameAsync(username);
                return result;
            }
            catch (FileStorageException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<AuthenticateResponse>> SignUpAsync(RegisterModel model)
        {
            try
            {
                var result = await _authService.SignUpAsync(model); 
                return Ok(result);
            }
            catch (FileStorageException ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticateResponse>> LogInAsync(AuthenticateModel model)
        {
            try
            {
                var result = await _authService.LogInAsync(model);
                return Ok(result);
            }
            catch (FileStorageException ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
