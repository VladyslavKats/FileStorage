using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.EF
{
    public static class FileStorageInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager , RoleManager<IdentityRole> roleManager) {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));

            string adminEmail = "admin@gmail.com";
            string password = "Admin_123";
            string name = "admin";
            User admin = new User { Email = adminEmail, UserName = name};
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
           
            
        }
    }
}
