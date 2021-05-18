using System;
using System.Text.Json.Serialization;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Purchases
{
    public class InvDetailsRes
    {
        public int? Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public int InvoiceId { get; set;}
        public int ProductId { get; set;}
        public DateTime LastUpdate { get; set; }



    }
}