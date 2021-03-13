namespace ALBAB.Entities.OrderAggregate
{
    public class OrderMethod : BaseEntity
    {
       /*  we don't need a constructor because our users are just going to 
        select a order method and we'll use the delivery method I.D. to decide which delivery method this
        is going to be in our order */
        
        public string ShortName {get;set;}
        public string OrderTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
    }
}