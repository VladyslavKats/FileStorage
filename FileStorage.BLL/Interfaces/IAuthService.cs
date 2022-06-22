using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> LogInAsync(AuthenticateModel model);

        Task<bool> SignUpAsync(RegisterModel model, string urlForConfirmation);

        Task<bool> CheckUserNameAsync(string userName);

        Task<bool> ConfirmEmailAsync(string userName , string token);
    }
}
