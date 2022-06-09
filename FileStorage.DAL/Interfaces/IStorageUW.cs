using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    public interface IStorageUW
    {

        UserManager<User> UserManager { get; }

        RoleManager<IdentityRole> RoleManager { get; } 

        IDocumentRepository Documents { get; }

        Task SaveChangesAsync();
    }
}
