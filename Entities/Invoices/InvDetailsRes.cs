using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Invoices
{
    public class InvDetailsRes
    {
        [RequiredGreaterThanZero]
        public int? Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public decimal  Cost { get; set; }
        public int InvoiceId { get; set;}
        public int? ProductId { get; set;}
        public DateTime LastUpdate { get; set; }
         public string Description { get; set;}


    }
}