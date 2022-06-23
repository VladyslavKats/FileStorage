using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// Defines methods for sending letters by email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends message by email
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <returns></returns>
        Task SendAsync(MailMessage message);
    }
}
