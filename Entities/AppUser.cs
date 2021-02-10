using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ALBaB.Entities
{
    public class AppUser : IdentityUser<int>
    {
     public ICollection<AppUserRole> UserRoles { get; set; }  
        
    }
}