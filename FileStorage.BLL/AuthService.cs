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


        public async Task<AuthenticateResponse> LogIn(AuthenticateModel model)
        {
            checkAuthenticateModel(model);
             var user = await _context.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
                throw new ArgumentException("User doesnt exist");
            if (!await _context.UserManager.CheckPasswordAsync(user, model.Password))
                throw new ArgumentException("Incorrect password!");
            string token = getToken(user);
            return new AuthenticateResponse { Token = token, UserName = user.UserName };
        }

        private void checkAuthenticateModel(AuthenticateModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentNullException(nameof(model), "Incorrect data!");
        }






        public async Task<AuthenticateResponse> SignUp(RegisterModel model)
        {
            checkRegisterModel(model);
            var userExist = await _context.UserManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                throw new ArgumentException("User has already existed!");
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _context.UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded) {
                await _context.UserManager.AddToRoleAsync(user, "User");
            }
            string token = getToken(user); 
            return new AuthenticateResponse { Token = token , UserName = user.UserName};
        }


        private void checkRegisterModel(RegisterModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserName)
               || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
                throw new ArgumentNullException(nameof(model), "Incorrect data!");
        }



        private string getToken(User user)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Role , authParams.DefaultRole)
            };

            var token = new JwtSecurityToken(
                    authParams.Issuer,
                    authParams.Audience,
                    claims,
                    expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
