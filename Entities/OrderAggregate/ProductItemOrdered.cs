using Microsoft.EntityFrameworkCore;

namespace ALBAB.Entities.OrderAggregate
{
     [Owned]
    public class ProductItemOrdered
    {
        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            this.pictureUrl = pictureUrl;
        }

        /*  this is just going to act as a snapshot of our order at the time it or our product
         item at the time it was placed just based on the fact that the product name might change the product
         image might change but we wouldn't want to change it as well inside our orders so we don't want to relation
         between our order and our product item we're going to store the values as it was when it was ordered
         into our database.
         this is not going to have an I.D. because it's going to be owned by the order itself. */
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string pictureUrl { get; set; }


    }
}