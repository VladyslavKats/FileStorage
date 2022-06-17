using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
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

        public AuthService(IStorageUW context , IOptions<AuthOptions> options)
        {
            _context = context;
            _options = options;
        }


        public async Task<AuthenticateResponse> LogInAsync(AuthenticateModel model)
        {
            checkAuthenticateModel(model);
            var user = await _context.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
                throw new FileStorageAuthenticateException("User doesnt exist");
            if (!await _context.UserManager.CheckPasswordAsync(user, model.Password))
                throw new FileStorageAuthenticateException("Incorrect password!");
            string token = await getTokenAsync(user);
            bool isAdmin = await  _context.UserManager.IsInRoleAsync(user, "Admin");
            return new AuthenticateResponse { Token = token, UserName = user.UserName , IsAdmin = isAdmin , UserId = user.Id };
        }

        private void checkAuthenticateModel(AuthenticateModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                throw new FileStorageArgumentException( "Incorrect data!");
        }






        public async Task<AuthenticateResponse> SignUpAsync(RegisterModel model)
        {
            checkRegisterModel(model);
            var userExist = await _context.UserManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                throw new FileStorageArgumentException("User has already existed!");
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _context.UserManager.CreateAsync(user, model.Password);

           

            if (!result.Succeeded) {
                throw new FileStorageArgumentException("Incorrect data!");
            }
            await _context.UserManager.AddToRoleAsync(user, "User");
            await _context.Accounts.CreateAsync(user.Id);
            await _context.Accounts.SaveAsync();
            string token = await getTokenAsync(user);
            bool isAdmin = await _context.UserManager.IsInRoleAsync(user, "Admin");
            return new AuthenticateResponse { Token = token , UserName = user.UserName , UserId = user.Id};
        }


        private void checkRegisterModel(RegisterModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserName)
               || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
                throw new FileStorageArgumentException( "Incorrect data!");
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
                throw new FileStorageArgumentException("incorrect username!");
            }

            var user = await _context.UserManager.FindByNameAsync(userName);
            return user != null;
        }
    }
}
