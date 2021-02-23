using System.ComponentModel.DataAnnotations;

namespace ALBaB.Entities.DTOs
{
    public class LoginDto
    {
       
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }




    }
}