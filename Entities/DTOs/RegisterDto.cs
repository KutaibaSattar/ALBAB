using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class RegisterDto
    {
      [Required]
       public string UserName { get; set; } 
      [Required]
       public string PasswordHash { get; set; }
    }
}