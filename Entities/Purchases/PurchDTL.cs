using System;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Purchases
{
    public class InvDetail : BaseEntity
    {


        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public DateTime LastUpdate { get; set; }
        public int InvoiceId { get; set;}
        public Invoice  Invoice { get; set; }
        public int ProductId { get; set;}
        public Product Product { get; set; }



    }
}