using System;
using System.ComponentModel.DataAnnotations;

namespace ALBaB.Entities.DTOs
{
    public class MemberDto
    {
     
     public int Id { get; set; }
      [Required(ErrorMessage = "You must provide a phone number")]
      [Phone]
      
      public string UserName { get; set; }
      public string PhoneNumber { get; set; }
      public DateTime Created { get; set; }   = DateTime.Now;
      public DateTime LastActive { get; set; } =  DateTime.Now; 
      public string Introduction { get; set; }
      public string LookingFor { get; set; }
      public string Interests { get; set; }   
    }
}