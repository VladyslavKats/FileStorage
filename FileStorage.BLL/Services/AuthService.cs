using FileStorage.BLL.Exceptions;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using ArgumentException = FileStorage.BLL.Exceptions.ArgumentException;

namespace FileStorage.BLL
{
    public class AuthService : IAuthService
    {
        private readonly IStorageUW _context;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IStorageUW context ,IJwtTokenGenerator tokenGenerator , IEmailService emailService)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
        }

        public async Task<AuthenticateResponse> LogInAsync(AuthenticateModel model)
        {
            var user = await _context.Users.FindByNameAsync(model.UserName);
            if (user == null)
                throw new AuthenticateException("User does not exist");
            if (!await _context.Users.CheckPasswordAsync(user, model.Password))
                throw new AuthenticateException("Incorrect password");
            if (!user.EmailConfirmed)
            {
                throw new AuthenticateException("Confirm email");
            }
            var roles = await _context.Users.GetRolesAsync(user);
            string token = await _tokenGenerator.GenerateAsync(user.Id , roles);
            return new AuthenticateResponse { 
                Token = token,
                UserName = user.UserName
            };
        }

        public async Task SignUpAsync(RegisterModel model , string urlForConfirmation)
        {
            var userExist = await _context.Users.FindByEmailAsync(model.Email);
            if (userExist != null)
                throw new Exceptions.ArgumentException("User has already existed");
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _context.Users.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new RegisterException("Error was occured while registration");
            await _context.Users.AddToRoleAsync(user, "User");
            var token = await _context.Users.GenerateEmailConfirmationTokenAsync(user);
            await SendCofirmationToken(token , user.UserName , user.Email, urlForConfirmation);
            await _context.Accounts.AddAsync(new Account { Id = user.Id });
            await _context.CommitAsync();
        }

        private async Task SendCofirmationToken(string token , string userName , string email, string url)
        {
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            string urlToClick = $"{url}api/Account/confirmEmail?username={userName}&token={codeEncoded}";
            string body = $"Please confirm your email - <a href=\"{urlToClick}\">confirm</a>";
            var message = new MailMessage
            {
                Subject = "Confirm email",
                To = email,
                Body = body
            };
            await _emailService.SendAsync(message);
        }

        public async Task<bool> CheckUserNameAsync(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Incorrect user name");
            }
            var user = await _context.Users.FindByNameAsync(userName);
            return user != null;
        }

        public async Task ConfirmEmailAsync(string userName, string token)
        {
            var user = await _context.Users.FindByNameAsync(userName);
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            var result = await _context.Users.ConfirmEmailAsync(user, codeDecoded);
            if (!result.Succeeded)
            {
                throw new ConfirmationException("Error was ocurred while confirmation email");
            }
        }
    }
}
