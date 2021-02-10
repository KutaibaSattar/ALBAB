
using Microsoft.AspNetCore.Identity;

namespace ALBaB.Entities
{
    public class AppUserRole : IdentityUserRole<int> // many-to-many relation between AppUser and AppRole
    {
        public AppUser User { get; set; }   
        public AppRole Role { get; set; }
        
    }
}