using System;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Purchases
{
    public class PurchDTL : BaseEntity
    {
       
        
        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public DateTime LastUpdate { get; set; }
        public int PurchHDRId { get; set;}
        public PurchHDR  PurchHDR { get; set; }
        public int ProductId { get; set;}
        public Product Product { get; set; }


    }
}