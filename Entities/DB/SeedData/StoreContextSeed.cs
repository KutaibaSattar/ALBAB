using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.Products;
using Microsoft.Extensions.Logging;

namespace ALBAB.Entities.DB.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory){
              try
        {
             if (!context.dbAccounts.Any())
            {
                var accountData = File.ReadAllText("../ALBAB/Entities/DB/SeedData/product/account.json");
                var accounts = JsonSerializer.Deserialize<List<dbAccounts>>(accountData);
               foreach (var item in accounts)
               {
                   context.dbAccounts.Add(item);
               }

            }
            
            if (!context.brands.Any())
            {
                var brandsData = File.ReadAllText("../ALBAB/Entities/DB/SeedData/product/brand.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
               foreach (var item in brands)
               {
                   context.brands.Add(item);
               }

            }
            /*  if (!context.models.Any())
            {
                var typesData = File.ReadAllText("../ALBAB/Entities/DB/SeedData/product/type.json");
                var types = JsonSerializer.Deserialize<List<Model>>(typesData);
               foreach (var item in types)
               {
                   context.models.Add(item);
               }

            }
             if (!context.products.Any())
            {
                var productsData = File.ReadAllText("../ALBAB/Entities/DB/SeedData/product/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
               foreach (var item in products)
               {
                   context.products.Add(item);
               }

            }   */
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