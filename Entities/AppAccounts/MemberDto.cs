using System;
using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities
{
    public class MemberDto
    {
     
     public int Id { get; set; }
      [Required(ErrorMessage = "You must provide a phone number")]
      [Phone]
      public string UserId { get; set; }
      public string DisplayName { get; set; }
      public string PhoneNumber { get; set; }
      public DateTime Created { get; set; }   = DateTime.Now;
      public DateTime LastActive { get; set; } =  DateTime.Now; 
      public string Introduction { get; set; }
      public string LookingFor { get; set; }
      public string Interests { get; set; }  

      
    }
}