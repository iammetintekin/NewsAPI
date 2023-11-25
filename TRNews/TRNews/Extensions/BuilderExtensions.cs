using Microsoft.EntityFrameworkCore;
using TRNews.Infrastructure.EntityFramework;

namespace TRNews.Extensions
{
    public static class BuilderExtensions
    {
        public static IHost MigrateDB(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var projectContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                {
                    try
                    {
                        projectContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
