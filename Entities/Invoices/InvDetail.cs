using System;
using System.ComponentModel.DataAnnotations.Schema;
using ALBAB.Entities.Products;

namespace ALBAB.Entities.Invoices
{
    public class InvDetail : BaseEntity
    {


        public decimal Quantity { get; set; }
        public decimal  Price { get; set; }
        public decimal  Cost { get; set; }
        public DateTime LastUpdate { get; set; }
        public int InvoiceId { get; set;}
        public Invoice  Invoice { get; set; }

        public int? ProductId { get; set;}
        public Product Product { get; set; }
        public string Description { get; set;}



        [NotMapped]
        public decimal TotalValue
        {
            get { return Quantity * Price; }
        }



    }
}