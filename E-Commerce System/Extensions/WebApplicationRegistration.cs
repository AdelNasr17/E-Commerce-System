using Domain.Contracts;
using E_Commerce_System.Middleware;

namespace E_Commerce_System.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            // Method to seed the database
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializerAsync();
            await dbInitializer.IdentityDataSeedingAsync();

            return app;

        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
