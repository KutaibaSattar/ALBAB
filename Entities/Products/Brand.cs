
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ALBAB.Entities.Products
{
    public class Brand : BaseEntity
    {
       

        public string Name { get; set; }

        public ICollection<Model> models {get;set;}

         public Brand()
        {
            
            models = new Collection<Model>();
        }



    }
}