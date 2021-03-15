using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.DB;
using System.ComponentModel.DataAnnotations;

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
       
      public Address Address {get;set;}    
        public OrderMethod  OrderMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        
        [StringLength(5)]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }

        public decimal GetTotal()
        {
           return Subtotal + OrderMethod.Price;
        }

        public Order (){
                OrderItems = new Collection<OrderItem>();
        }


        
    }
}