using E_Commerce.API.Extensions;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region DI Container
            var builder = WebApplication.CreateBuilder(args);
            // Web API services extension method
            builder.Services.AddWebApiServices();

            //infrastructure services extension method
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //core services extension method
            builder.Services.AddCoreServices(); 
            #endregion


            var app = builder.Build();

            #region Pipliens
            //data seeding extension method
            await app.SeedDatabaseAsync();

            // Configure the HTTP request pipeline.
            app.UseExepctionHandlingMiddleware();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
