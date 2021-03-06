using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.DB.SeedData;

namespace ALBAB
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
          
           var host =  CreateHostBuilder(args).Build();
           using var scope = host.Services.CreateScope();
           var services = scope.ServiceProvider;
           var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                
               try
            {
              
               var context = services.GetRequiredService<DataContext>();
               var userManager = services.GetRequiredService<UserManager<AppUser>>();
               var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                
                //await context.Database.MigrateAsync();
                
                await Seed.SeedUsersAsync(userManager,roleManager,context); 
                await StoreContextSeed.SeedAsync(context,loggerFactory);
                
            }
            catch (Exception ex)
            {
                
                 var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                 logger.LogError(ex.Message);
            } 

       
           
           host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
