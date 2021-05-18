using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities
{
    public class RegisterDto
    {
      [Required]
    public string Name { get; set; }

     [Required]
     //[RegularExpression("^[0-9]*$", ErrorMessage = "Phone or code must be numeric")]
     public string KeyId { get; set; }

      [Required]
      public string Password { get; set; }



    }
}