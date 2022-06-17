
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Models
{
    public class User : IdentityUser
    {
        public Account Account { get; set; }
    }
}
