using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Entities.ProductModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext, 
        RoleManager<IdentityRole> _roleManager ,
        UserManager<User> _userManager) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            try
            {
                if ( pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = await File.ReadAllTextAsync("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                    }

                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = await File.ReadAllTextAsync("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);

                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                    }

                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = await File.ReadAllTextAsync("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    if (products != null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }

                }
                _dbContext.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SeedIdentityDataAsync()
        {
            try 
            {
                if(! _roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gamil.com",
                        PhoneNumber = "01150803845"
                    };

                    var SuperAdminUser = new User
                    {
                        DisplayName = "superAdmin",
                        UserName = "superAdmin",
                        Email = "superAdmin@gamil.com",
                        PhoneNumber = "01150803845"
                    };

                    await _userManager.CreateAsync(adminUser, "Admin@123");
                    await _userManager.CreateAsync(SuperAdminUser, "SuperAdmin@123");

                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while seeding identity data", ex);
            }
        }
    }
}
