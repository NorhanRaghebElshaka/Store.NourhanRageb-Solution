using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Repository
{
    public class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _context)
        {
            if (_context.Brands.Count() == 0)
            {
                // Barnd
                //1.Read Data From Josn File

                var brandData = File.ReadAllText(@"..\Store.NourhanRageb.Repository\Data\DataSeed\brands.josn");

                // 2. convert Json String To List<T>

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                // 3. Seed Data To DataBase
                if (brands is not null && brandData.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();

                }
            }

            if (_context.Types.Count() == 0)
            {
                // Types
                //1.Read Data From Josn File

                var typesData = File.ReadAllText(@"..\Store.NourhanRageb.Repository\Data\DataSeed\types.josn");

                // 2. convert Json String To List<T>

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                // 3. Seed Data To DataBase
                if (types is not null && typesData.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();

                }
            }

            if (_context.Products.Count() == 0)
            {
                // products
                //1.Read Data From Josn File

                var productsData = File.ReadAllText(@"..\Store.NourhanRageb.Repository\Data\DataSeed\products.josn");

                // 2. convert Json String To List<T>

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                // 3. Seed Data To DataBase
                if (products is not null && productsData.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();

                }
            }

        }
    }
}
