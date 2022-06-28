using FileStorage.BLL.Common;
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
        public async Task<ActionResult<bool>> CheckUserNameAsync(string username)
        {
            try
            {
               
                bool result = await _authService.CheckUserNameAsync(username);
                return result;
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUpAsync(RegisterModel model)
        {
            try
            {
                var request = HttpContext.Request;
               
                var url = $"{request.Scheme}://{request.Host}/";
                var result = await _authService.SignUpAsync(model , url);
                if (result) {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }



        [HttpGet]
        [Route("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery]string username , [FromQuery]string token)
        {
            try
            {
                var result = await _authService.ConfirmEmailAsync(username , token);
                if (result)
                {
                    return Ok("Email has been confirmed!");
                }
                else
                {
                    return BadRequest("Error.Try later");
                }
            }
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteAsync(string userName)
        {
            try
            {
                await _authService.RemoveUserAsync(userName);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
