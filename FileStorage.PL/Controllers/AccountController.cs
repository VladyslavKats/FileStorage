using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
       
        public AccountController(IAuthService authService )
        {
            _authService = authService;
            
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> CheckUserNameAsync(string username)
        {
            var result = await _authService.CheckUserNameAsync(username);
            return Ok(result);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUpAsync([FromBody]RegisterModel model)
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/";
            await _authService.SignUpAsync(model , url);
            return Ok();
        }

        [HttpGet]
        [Route("confirmemail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery]string username , [FromQuery]string token)
        {
            await _authService.ConfirmEmailAsync(username , token);
            return Ok("Email has been confirmed!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticateResponse>> LogInAsync(AuthenticateModel model)
        {
            var result = await _authService.LogInAsync(model);
            return Ok(result);
        }
    }
}
