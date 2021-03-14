namespace ALBAB.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem( decimal price, decimal quantity)
        {
            Price = price;
            Quantity = quantity;
        }

        public Order Order  { get; set; }
       
        public int OrderId  { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        
    }
}