using System.ComponentModel.DataAnnotations;

namespace ALBaB.Entities.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        /*  [Required]
         public string Username { get; set; } */

        [Required]
        public string Password { get; set; }


    }
}