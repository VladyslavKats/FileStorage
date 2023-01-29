using FileStorage.BLL.Models;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> LogInAsync(AuthenticateModel model);

        Task SignUpAsync(RegisterModel model, string urlForConfirmation);

        Task<bool> CheckUserNameAsync(string userName);
        
        Task ConfirmEmailAsync(string userName , string token);
            }
}
