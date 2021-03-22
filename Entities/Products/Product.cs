using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ALBAB.Entities.Purchases;

namespace ALBAB.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        /* public DateTime LastUpdate { get; set; } */
        public int ModelId { get; set; }
        public Model Model { get; set; }
         public ICollection<PurchDTL> PurchDTLs {get;set;}
              
       
    }

   
}