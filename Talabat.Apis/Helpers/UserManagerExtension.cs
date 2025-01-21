using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.Apis.Helpers
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> GetAdressByEmailAsync (this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(i => i.Email == email);
            return user;
        }
    }
}
