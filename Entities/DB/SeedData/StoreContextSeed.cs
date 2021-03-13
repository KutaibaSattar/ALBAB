using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ALBAB.Entities.Products;
using Microsoft.Extensions.Logging;

namespace ALBAB.Entities.DB.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory){
              try
        {
           /*  if (!context.productBrands.Any())
            {
                var brandsData = File.ReadAllText("../product/brand.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
               foreach (var item in brands)
               {
                   context.productBrands.Add(item);
               }

            } */
             if (!context.productType.Any())
            {
                var typesData = File.ReadAllText("../ALBAB/Entities/DB/SeedData/product/type.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
               foreach (var item in types)
               {
                   context.productType.Add(item);
               }

            }
              /*  if (!context.product.Any())
            {
                var productsData = File.ReadAllText("../product/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
               foreach (var item in products)
               {
                   context.product.Add(item);
               }

            } */

            await context.SaveChangesAsync();
        }
        catch (System.Exception ex)
        {
            
           var logger = loggerFactory.CreateLogger<StoreContextSeed>();
           logger.LogError(ex.Message);
        }
        }

      
    }
}