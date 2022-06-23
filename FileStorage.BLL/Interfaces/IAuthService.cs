using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// Defines basic methods for authentication , registration and  managing accounts in file storage
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Allows user to login in the storage
        /// </summary>
        /// <param name="model">Model wich contains login and password of user</param>
        /// <returns>Data of user and jwt token</returns>
        Task<AuthenticateResponse> LogInAsync(AuthenticateModel model);
        /// <summary>
        /// Allows registrate new user and creates account for him
        /// </summary>
        /// <param name="model">Data of user</param>
        /// <param name="urlForConfirmation">Basic application url for confirmation token</param>
        /// <returns>True if user was created , false - if was not</returns>
        Task<bool> SignUpAsync(RegisterModel model, string urlForConfirmation);

        /// <summary>
        /// Allows to find out user`s name is used or not
        /// </summary>
        /// <param name="userName">User`s name</param>
        /// <returns>True if that name is has already used , false - if not</returns>
        Task<bool> CheckUserNameAsync(string userName);
        /// <summary>
        /// Allows confirm email of user
        /// </summary>
        /// <param name="userName">User`s name</param>
        /// <param name="token">Token for confirmation</param>
        /// <returns>True if user confirmed email successfully , false - if not</returns>
        Task<bool> ConfirmEmailAsync(string userName , string token);
        /// <summary>
        /// Removes user and his account , deletes all his files
        /// </summary>
        /// <param name="userName">User`s name which will be deleted</param>
        /// <returns></returns>
        Task RemoveUserAsync(string userName);
    }
}
