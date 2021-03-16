using System;

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
        
       
    }

   
}