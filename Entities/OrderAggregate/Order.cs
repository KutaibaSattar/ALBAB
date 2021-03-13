using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALBAB.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
       /*  public Order()
        {
            
        }

        public Order(IEnumerable<OrderItem> orderItems,string buyerEmail, OrderAddress orderAddress,
         OrderMethod orderMethod,  decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            OrderAddress = orderAddress;
            OrderMethod = orderMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
           
        } */

        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderAddress OrderAddress { get; set; }
        public OrderMethod  OrderMethod { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        
        [Column(TypeName = "nvarchar(10)")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
           return Subtotal + OrderMethod.Price;
        }


        
    }
}