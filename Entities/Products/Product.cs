using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using ALBAB.Entities.Purchases;

namespace ALBAB.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Quantity { get; set; }
        public string PictureUrl { get; set; }
       public int ModelId { get; set; }
        public Model Model { get; set; }
         public ICollection<InvDetail> PurchDTLs {get;set;}
        public DateTime LastUpdate { get; set; }

        //[NotMapped]
        // public decimal TotalValue
        // {
        //     get { return Quantity * Price; }
        // }




    }


}