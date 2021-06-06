using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities
{
    public class RegisterDto
    {
      [Required]
    public string Name { get; set; }

     [Required]
     //[RegularExpression("^[0-9]*$", ErrorMessage = "Phone or code must be numeric")]
     public string KeyId { get; set; }



     public UserType? type { get; set; } = UserType.Client;


      [Required]
      public string Password { get; set; }



    }
}