using System;
using ALBaB.Data;
using ALBaB.Entities;
using ALBaB.Entities.DTOs;
using ALBaB.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ALBaB.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AppServices (this IServiceCollection /* class to be extend*/ services,
         IConfiguration config /* for connection string*/ )
        {
            
           
            services.AddScoped<ITokenService,TokenService>();
           
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseMySql(config.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21)));
            }
            );

              services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ALBaB", Version = "v1" });
            });

          

            return services;

        }
        
    }
}