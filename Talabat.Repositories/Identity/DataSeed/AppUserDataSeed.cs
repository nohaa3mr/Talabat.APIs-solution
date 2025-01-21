using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repositories.Identity.DataSeed
{
    public static class AppUserDataSeed
    {
        public static async Task AddUserDataSeedAsync(UserManager<AppUser> userManager)
        {
            if (! userManager.Users.Any()) 
            {
                var AppUser = new AppUser()
                {
                    DisplayName = "Noha Amr",
                    Email = "nohaa3mr@gmail.com",
                    UserName = "Noha.Amr",
                    PhoneNumber = "123456790" , 

                };
                await userManager.CreateAsync(AppUser, "P@ssw0rd");
            }

          
        }
    }
}
