using System;

namespace ALBAB.Entities.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string PictureUrl { get; set; }
        public int ModelId { get; set; }
       public string Model { get; set; }
       public string Brand { get; set; }
       public DateTime LastUpdate { get; set; }

      }
}