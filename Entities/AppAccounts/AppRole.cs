using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ALBAB.Entities.AppAccounts
{
    public class AppRole : IdentityRole<int>
    {
       public ICollection<AppUserRole> UserRoles { get; set; }  
    }
}