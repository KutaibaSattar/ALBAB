using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ALBAB.Entities.AppAccounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Extensions
{
    public static class UserManagerExtensions
    {
      /*   public static async Task<AppUser> FindUserByEmailByClaimPrincipleWithAssressAsync(this UserManager<AppUser> 
        input , ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault( x => x.Type == ClaimTypes.Email)?.Value;
            
            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync( x => x.Email == email);

        }
 */
        public static async Task<AppUser> FindRoleByRoleClaim ( this UserManager<AppUser>
        input , ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault( x => x.Type == ClaimTypes.Email)?.Value;
            
             return await input.Users.SingleOrDefaultAsync( x => x.Email == email);
        }
    }
}