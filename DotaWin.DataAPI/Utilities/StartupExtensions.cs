using Microsoft.EntityFrameworkCore;

namespace DotaWin.DataAPI.Utilities;
public static class StartupExtensions
{
    public static IHost RunDatabaseMigrations<T>(this IHost webHost) where T : DbContext
    {
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
        return webHost;
    }
}
