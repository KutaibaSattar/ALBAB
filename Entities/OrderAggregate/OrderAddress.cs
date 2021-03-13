using Microsoft.EntityFrameworkCore;

namespace ALBAB.Entities.OrderAggregate
{
     [Owned]
    public class OrderAddress
    {
        public OrderAddress(string line1, string line2, string region, string city, string country)
        {
            Line1 = line1;
            Line2 = line2;
            Region = region;
            City = city;
            Country = country;
        }

    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Country { get; set;}
        
    }
}