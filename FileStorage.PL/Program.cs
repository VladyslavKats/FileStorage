using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace FileStorage.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            using (var scope = builder.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                var dbInitializer = services.GetRequiredService<IDatabaseInitializer>();
                try
                {
                    logger.LogInformation("Initializing database...");
                    dbInitializer.Initialize();
                    logger.LogInformation("Database is initialized");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex , ex.Message);
                }
            }
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
