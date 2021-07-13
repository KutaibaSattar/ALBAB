namespace ALBAB.Entities.AppAccounts
{
    public class AddressRes
    {
    public int Id {get; set;}
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Country { get; set;}
    public int AppUserId { get; set;}

    }
}