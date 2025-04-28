
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using System.Text.Json;


namespace Persistence.Data.DataSeeding
{
    public class DbInitializer(APPDbContext _dbContext, UserManager<ApplicationUser> _userMananger,
       RoleManager<IdentityRole> _roleManager , StoreIdentityDbContext _IdentityDbContext ) : IDbInitializer
    {
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
        public async Task IdentityDataSeedingAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SupperAdmin"));
                }

                if (!_userMananger.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "MohamedTarek",
                        PhoneNumber = "012345675575",
                        UserName = "MohamedTarek"
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "SalmaMohamed",
                        PhoneNumber = "012345675575",
                        UserName = "SalmaMohamed"
                    };

                    await _userMananger.CreateAsync(user01, "P@ssw0rd");
                    await _userMananger.CreateAsync(user02, "P@ssw0rd");

                    await _userMananger.AddToRoleAsync(user01, "Admin");
                    await _userMananger.AddToRoleAsync(user02, "SupperAdmin");

                }

                await _IdentityDbContext.SaveChangesAsync();
            }catch(Exception ex)
            {

            }

            


        }

    }
}
