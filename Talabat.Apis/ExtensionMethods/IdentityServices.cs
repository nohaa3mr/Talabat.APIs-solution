using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;
using Talabat.Repositories.Identity;
using Talabat.Services.TokenService;

namespace Talabat.Apis.ExtensionMethods
{
    public static class IdentityServices
    {

        public static IServiceCollection AddIdentity(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(Options => 
            { Options.TokenValidationParameters = new TokenValidationParameters()
            {   ValidateIssuer = true, 
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true, 
                ValidAudience = configuration["Jwt:Audience"],
                ValidateLifetime = true ,
                ValidateIssuerSigningKey =true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt : Key"]))
            }; 
            
            });
            Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddScoped<ITokenService, TokenService>();
            return Services;
        }
    }
}
