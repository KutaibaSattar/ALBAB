using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ALBAB.Entities.AppAccounts
{
    public class AppUserRes
    {
      public int Id{get;set;}
      public string Name { get; set;}
      public string KeyId { get; set;}
       public AccountType type { get; set; }
      public string PhoneNumber {get;set;}
      public string Token { get; set; }
      public string Email { get; set; }

       public ICollection<AddressRes> Address { get; set; }
      public AppUserRes(){

        Address = new Collection<AddressRes>();

      }


    }
}