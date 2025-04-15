using Domain.Contracts;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private readonly APPDbContext _dbContext;

        public DbInitializer(APPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializerAsync()
        {
            try
            {
                //if(_dbContext.Database.GetPendingMigrations().Any())
                //{
                    //await _dbContext.Database.MigrateAsync();
                    if(!_dbContext.ProductTypes.Any())
                    {
                        var typeData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\types.json");

                        var Types= JsonSerializer.Deserialize<List<ProductType>>(typeData);

                        if( Types is not null&&Types.Any())
                        {
                            await _dbContext.AddRangeAsync(Types);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    if (!_dbContext.ProductBrands.Any())
                    {
                        var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\brands.json");

                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                        if (brands is not null && brands.Any())
                        {
                            await _dbContext.AddRangeAsync(brands);
                            await _dbContext.SaveChangesAsync();

                        }
                    }

                    if (!_dbContext.Products.Any())
                    {
                        var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\products.json");

                        var product = JsonSerializer.Deserialize<List<Product>>(productsData);

                        if (product is not null && product.Any())
                        {
                            await _dbContext.AddRangeAsync(product);
                            await _dbContext.SaveChangesAsync();

                        }
                    }
               // }
            }
            catch(Exception ) 
            {
                throw;
            }
        }
    }
}
