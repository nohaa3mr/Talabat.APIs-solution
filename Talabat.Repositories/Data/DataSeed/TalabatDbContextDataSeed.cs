using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositories.Data.DataSeed
{
    public static class TalabatDbContextDataSeed
    {
        public static async Task SeedAsync(TalabatDbContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var ProductBrandsData = File.ReadAllText("../Talabat.Repositories/Data/DataSeed/brands.json");
                var Brand = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
                if (Brand?.Count > 0)
                {
                    foreach (var item in Brand)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.ProductTypes.Any())
            {
                var ProductTypeData = File.ReadAllText("../Talabat.Repositories/Data/DataSeed/types.json");
                var proType = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                if (proType?.Count > 0)
                {
                    foreach (var productType in proType)
                    {
                        await dbContext.Set<ProductType>().AddAsync(productType);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }

              if (!dbContext.Products.Any()) 
              { 
            var ProductData = File.ReadAllText("../Talabat.Repositories/Data/DataSeed/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
              if (products?.Count > 0)
              {
                foreach (var product in products)
                {
                        product.Discription = product.Discription ?? "No description available";
                        product.PictureURL = product.Discription ?? " PictureURL isn't available";
                        await dbContext.Set<Product>().AddAsync(product);
                }
                await dbContext.SaveChangesAsync();
              }
              }
            }
    }
}
