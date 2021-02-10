using Microsoft.EntityFrameworkCore;

namespace ALBaB.Entities
{
     [Owned]
    public class Address
    {
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Country { get; set;}
       

   }
}