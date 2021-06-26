using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.AppAccounts;
//using ALBAB.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Entities.DB.SeedData
{
    public class Seed
    {
        public static async Task SeedUsersAsync (UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext context)
        {

          /* if (! context.OrderMethods.Any())
          {
              var item =
              context.OrderMethods.Add( new OrderMethod {ShortName="Installation",
              Description= "Installation and Services", Price = 100});
              context.SaveChanges();

          } */


           if (!userManager.Users.Any())
           {
               var user = new AppUser
               {
                  //UserName =  "kutaiba",

                  UserName ="971551234561",
                  Name ="قتيبة",
                  Email = "md@seraime.com",
                  PhoneNumber ="+971 55 1234561",
                  type = AccountType.Client,

                 /*  Address =  new Address
                  {
                      Line1="Sharjah",
                      Line2 = "Majaz",
                      Country = "UAE",
                      City = "Dubai"

                  } */

               };
               await userManager.CreateAsync(user, "Pa$$w0rd" );

               var user2 = new AppUser
               {
                  UserName =  "971551234562",
                  Name="Huda Hussain",
                  Email = "info@seraime.com",
                  PhoneNumber ="+971 55 1234562",
                  type = AccountType.Client,

                 /*  Address =  new Address
                  {
                      Line1="Sharjah",
                      Line2 = "Majaz",
                      Country = "UAE",
                      City = "Dubai"

                  }
 */
               };
               await userManager.CreateAsync(user2, "Pa$$w0rd" );

                var user3 = new AppUser
               {
                 UserName =  "971551234563",
                 Name ="Abd ALRahman",
                 Email = "support@seraime.com",
                 PhoneNumber ="+971 55 1234563",
                  type = AccountType.Client,
                /*   Address =  new Address
                  {
                      Line1="Sharjah",
                      Line2 = "Majaz",
                      Country = "UAE",
                      City = "Dubai"

                  } */

               };
         await userManager.CreateAsync(user3, "Pa$$w0rd");

          var users = await userManager.Users.ToListAsync();

           if (!await userManager.Users.AnyAsync()) return;



                 var roles = new List<AppRole>
            {
                new  AppRole {Name = "Member"},
                new  AppRole {Name = "Admin"},
                new  AppRole {Name = "User"},

            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var userAccount in users){
                await userManager.AddToRoleAsync(userAccount, "User");

            }

             var admin = new AppUser
               {
                UserName =  "971551234560",
                Name ="MD",
                Email = "admin@seraime.com",
                PhoneNumber ="+971 55 1234560",
                type = AccountType.Client,


               };
               await userManager.CreateAsync(admin, "Pa$$w0rd");



            await userManager.AddToRolesAsync(admin, new[] {"Admin","User"});

        }


        }
    }
}