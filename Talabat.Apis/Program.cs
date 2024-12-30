
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.ErrorsHandler;
using Talabat.Apis.ExtensionMethods;
using Talabat.Apis.MappingProfiles;
using Talabat.Apis.Middlewares;
using Talabat.Core.Interfaces;
using Talabat.Repositories.Data;
using Talabat.Repositories.Data.DataSeed;
using Talabat.Repositories.Interfaces.Contract;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TalabatDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });





            builder.Services.AddApplicationService();
            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = Services.GetRequiredService<TalabatDbContext>();
                await dbcontext.Database.MigrateAsync();
                await TalabatDbContextDataSeed.SeedAsync(dbcontext);

            }
            catch (Exception ex)
            {

                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError("An error has been occured while running the application ");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();

            app.UseStaticFiles(); 
            app.MapControllers();

            app.Run();
        }
    }
}
