using System;
using System.ComponentModel.DataAnnotations;

namespace ALBaB.Entities.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Password { get; set; }
     

    }
}