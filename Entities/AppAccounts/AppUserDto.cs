namespace ALBAB.Entities.AppAccounts
{
    public class AppUserDto
    {
      public int Id{get;set;}
      public string Name { get; set;}
      public string KeyId { get; set;}
       public AccountType type { get; set; }
      public string PhoneNumber {get;set;}
      public string Token { get; set; }
      public string Email { get; set; }


    }
}