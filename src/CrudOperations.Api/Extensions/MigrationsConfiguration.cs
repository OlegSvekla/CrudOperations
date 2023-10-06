using CrudOperations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Api.Extensions
{
    public static class MigrationsConfiguration
    {
        public static IApplicationBuilder RunDbContextMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<CrudDbContext>>();

                logger.LogInformation("Database migration running...");

                try
                {
                    var context = serviceProvider.GetRequiredService<CrudDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            return app;
        }
    }
}
