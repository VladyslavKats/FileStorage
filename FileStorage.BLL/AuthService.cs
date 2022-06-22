using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    public class AuthService : IAuthService
    {
        private readonly IStorageUW _context;
        private readonly IOptions<AuthOptions> _options;
        private readonly IEmailService _emailService;

        public AuthService(IStorageUW context , IOptions<AuthOptions> options , IEmailService emailService)
        {
            _context = context;
            _options = options;
            _emailService = emailService;
        }


        public async Task<AuthenticateResponse> LogInAsync(AuthenticateModel model)
        {
            checkAuthenticateModel(model);
            var user = await _context.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
                throw new FileStorageAuthenticateException("User doesnt exist");
            if (!await _context.UserManager.CheckPasswordAsync(user, model.Password))
                throw new FileStorageAuthenticateException("Incorrect password");
            if (user.EmailConfirmed == false)
            {
                throw new FileStorageAuthenticateException("Confirm email");
            }
            string token = await getTokenAsync(user);
            bool isAdmin = await  _context.UserManager.IsInRoleAsync(user, "Admin");
            return new AuthenticateResponse { Token = token, UserName = user.UserName , IsAdmin = isAdmin , UserId = user.Id };
        }

        private void checkAuthenticateModel(AuthenticateModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                throw new FileStorageArgumentException( "Incorrect data");
        }






        public async Task<bool> SignUpAsync(RegisterModel model , string urlForConfirmation)
        {
            checkRegisterModel(model);
            var userExist = await _context.UserManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                throw new FileStorageArgumentException("User has already existed");
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _context.UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new FileStorageAuthenticateException();
            await _context.UserManager.AddToRoleAsync(user, "User");


            var token = await _context.UserManager.GenerateEmailConfirmationTokenAsync(user);

            await sendCofirmationToken(token , user.UserName , user.Email, urlForConfirmation);
            
            await _context.Accounts.CreateAsync(user.Id);
            await _context.Accounts.SaveAsync(); 
            return result.Succeeded;
        }

        private async Task sendCofirmationToken(string token , string userName , string email, string url)
        {
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            string urlToClick = $"{url}api/Account/confirmEmail?username={userName}&token={codeEncoded}";
            string body = $"Please confirm your email - <a href=\"{urlToClick}\">confirm</a>";
            await _emailService.SendAsync(new MailMessage {Subject = "Confirm email" , To = email , Body = body});
        }


        private void checkRegisterModel(RegisterModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserName)
               || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
                throw new FileStorageArgumentException( "Incorrect data");
        }



        private async Task<string> getTokenAsync(User user)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles =  await _context.UserManager.GetRolesAsync(user);

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name , user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                    authParams.Issuer,
                    authParams.Audience,
                    claims,
                    expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CheckUserNameAsync(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                throw new FileStorageArgumentException("incorrect username");
            }

            var user = await _context.UserManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task<bool> ConfirmEmailAsync(string userName, string token)
        {
            
            var user = await _context.UserManager.FindByNameAsync(userName);
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            
            var result = await _context.UserManager.ConfirmEmailAsync(user, codeDecoded);
            return result.Succeeded;
            
        }
    }
}
