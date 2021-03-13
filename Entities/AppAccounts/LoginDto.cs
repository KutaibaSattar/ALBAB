using System;
using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities.AppAccounts
{
    public class LoginDto
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Password { get; set; }
     

    }
}