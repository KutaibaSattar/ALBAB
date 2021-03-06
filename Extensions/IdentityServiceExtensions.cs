using System.Text;
using ALBAB.Entities.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Extensions
{
     /* inside here we'll make this a static class because it's going to contain extension methods */
    public static class IdentityServiceExtensions
    {
        // use (this) because we need to extend IserviceCollection 
        public static IServiceCollection IdentityServices( this IServiceCollection services, IConfiguration config)
        {
            
            services.AddIdentityCore<AppUser> (opt =>
            {
               opt.Password.RequireNonAlphanumeric = false; 
               opt.Password.RequireDigit =false;
               opt.Password.RequiredUniqueChars = 0;
               opt.Password.RequiredLength = 3;
               opt.Password.RequireLowercase = false;
               opt.Password.RequireUppercase = false;
               opt.User.RequireUniqueEmail = false;
                              //opt.User.AllowedUserNameCharacters = null;
             
                
               

            })
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddSignInManager<SignInManager<AppUser>>()
               .AddRoleValidator<RoleValidator<AppRole>>()
               .AddEntityFrameworkStores<DataContext>() ;

            
        // For Authentication Sheceme
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( options =>{
                options.TokenValidationParameters = new TokenValidationParameters
                {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer  = false,
                        ValidateAudience = false,

                };
                

            }); 
           
            services.AddAuthorization( opt =>{
                opt.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequiredUserRole", policy => policy.RequireRole("User"));

            });
            return services;

            
        }
        
    }
}