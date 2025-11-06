using Domain.Contracts;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app) 
        {
            var scope = app.Services.CreateScope();
            var objectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectOfDataSeeding.SeedDataAsync();
            await objectOfDataSeeding.SeedIdentityDataAsync();

            return app;
        }
        public static WebApplication UseExepctionHandlingMiddleware(this WebApplication app) 
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
