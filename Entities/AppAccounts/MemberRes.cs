using System;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities
{
    public class MemberRes
    {
      public int Id { get; set; }
      [Required(ErrorMessage = "You must provide a phone number")]
      [Phone]
      public string KeyId { get; set; }
      public string Name { get; set; }
      public AccountType type { get; set; }
      public string PhoneNumber { get; set; }
      public DateTime Created { get; set; }   = DateTime.Now;
      public DateTime LastActive { get; set; } =  DateTime.Now;
       public string LookingFor { get; set; }
      public string Interests { get; set; }

    }
}