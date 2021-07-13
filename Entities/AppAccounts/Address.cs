using ALBAB.Entities.AppAccounts;


namespace ALBAB.Entities.AppAccounts
{
    public class Address : BaseEntity
    {
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Country { get; set;}
    public int AppUserId { get; set;}
    public AppUser  AppUser { get; set; }


   }
}