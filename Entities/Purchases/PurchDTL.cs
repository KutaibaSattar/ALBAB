using System;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Purchases
{
    public class PurchDtl : BaseEntity
    {
       
        
        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public DateTime LastUpdate { get; set; }
        public int PurchHdrId { get; set;}
        public PurchHdr  PurchHdr { get; set; }
        public int ProductId { get; set;}
        public Product Product { get; set; }

       

    }
}