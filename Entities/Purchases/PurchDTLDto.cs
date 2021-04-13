using System;
using System.Text.Json.Serialization;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Purchases
{
    public class PurchDtlDto : BaseEntity
    {
        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public int PurchHdrId { get; set;}
        public int ProductId { get; set;}
        public DateTime LastUpdate { get; set; }
       
             

    }
}