using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities
{
    public class RegisterDto
    {
      [Required]
    public string DisplayName { get; set; } 
          
     [Required]
     //[RegularExpression("^[0-9]*$", ErrorMessage = "Phone or code must be numeric")]
     public string UserId { get; set; } 
      
      [Required]
      public string Password { get; set; }

      
      
    }
}