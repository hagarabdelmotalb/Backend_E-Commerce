using Domain.Contracts;
using Domain.Entities.ProductModule;
using System.Text.Json;

namespace Persistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
    }
}
