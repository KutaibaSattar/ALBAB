namespace ALBAB.Entities.Products
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set;}
 
    }
}