using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.BLL.Models
{
    public class AuthenticateResponse
    {
        public string UserName { get; set; }

        public string Token { get; set; }
    }
}
