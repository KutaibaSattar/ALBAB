using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALBaB.Data;
using ALBaB.Entities.DTOs;
using ALBaB.Errors;
using ALBaB.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ALBaB
{
    public class Startup
    {
        private IConfiguration _config { get; }
        public Startup(IConfiguration config)
        {
           _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
           services.AppServices(_config);
           services.IdentityServices(_config);


             
        
           services.AddControllers();
           
          
        // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           app.UseMiddleware<ExceptionMiddleware>();
           
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ALBaB v1"));
            }

          /*  1-  command in on a HTTP address, redirected to the HTTP endpoints. */
            app.UseHttpsRedirection();
           
           /* 2- set up routing.We've seen routing and action because 
           we were able to go from our browser weatherforecast endpoint
            and get to weatherforecast controller. */
           app.UseRouting();
         
         
         app.UseAuthentication();
         app.UseAuthorization(); 
       
       
       //  app.UseStaticFiles(); // for api static  like angular

          /* 3- we've got the middleware to actually use the endpoints and we've got a method
                here to map the controllers.And this takes a look inside our controllers
                 to see what endpoints are available Like [HttpGet]. */
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
             /*  app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    
                }
            }); */
        }
    }
}
