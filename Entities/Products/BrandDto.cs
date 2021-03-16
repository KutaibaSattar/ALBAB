using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ALBAB.Entities.Products
{
    public class BrandDto
    {   
        public int Id { get; set; }
        public string Name { get; set; }

         public ICollection<ModelDto> models {get;set;}

         public BrandDto()
        {
            
            models = new Collection<ModelDto>();
        }
      
    }
}