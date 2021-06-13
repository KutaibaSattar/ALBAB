using System;
using ALBAB.Entities;
using ALBAB.Entities.DB;
using ALBAB.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ALBAB.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AppServices (this IServiceCollection /* class to be extend*/ services,
         IConfiguration config /* for connection string*/ )
        {


            services.AddScoped<ITokenService,TokenService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddControllers().AddNewtonsoftJson();

          /*  services.AddControllersWithViews()
                        .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); */


            services.AddDbContext<DataContext>(opt =>
            {
               /*  opt.UseSqlite(config.GetConnectionString("SQLiteConnection"));

                opt.UseNpgsql(config.GetConnectionString("postgresConnection"));
 */
                //opt.UseMySql(config.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 0, 21)));

                opt.UseMySQL(config.GetConnectionString("MySqlConnection"));
            }
            );

              services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ALBAB", Version = "v1" });
            });



            return services;

        }

    }
}