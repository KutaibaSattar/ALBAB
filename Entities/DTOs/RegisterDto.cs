using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class RegisterDto
    {
      [Required]
       public string UserName { get; set; } 
          
     [Required]
     [RegularExpression("^[0-9]*$", ErrorMessage = "Phone must be numeric")]
     public string PhoneNumber { get; set; } 
      
      [Required]
      public string Password { get; set; }
      
    }
}