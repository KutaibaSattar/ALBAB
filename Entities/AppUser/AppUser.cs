using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ALBaB.Entities
{
    public class AppUser : IdentityUser<int>
    {
      [Required(ErrorMessage = "You must provide a phone number")]
      [Phone]
      public override string PhoneNumber { get; set; }
   
      public DateTime Created { get; set; }   = DateTime.Now;

       public DateTime LastActive { get; set; } =  DateTime.Now; 

       public Address Address {get;set;}    
      

     public ICollection<AppUserRole> UserRoles { get; set; }  
        
    }
}