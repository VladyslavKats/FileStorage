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

        Task<AuthenticateResponse> SignUpAsync(RegisterModel model);

        Task<bool> CheckUserNameAsync(string userName);
    }
}
