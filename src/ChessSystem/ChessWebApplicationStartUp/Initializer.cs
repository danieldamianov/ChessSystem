namespace ChessWebApplicationStartUp
{
    using System;
    using System.Threading.Tasks;
    using ChessWebApplication;
    using Infrastructure.Identity;
    using Infrastructure.Persistence;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public static class Initializer
    {
        public static IHost Initialize(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ChessApplicationDbContext>();
                context.Database.Migrate();

                var userManager = services.GetRequiredService<UserManager<ChessAppUser>>();

                Task
                    .Run(async () =>
                    {
                        await ChessApplicationDbContextSeed.SeedAsync(context, userManager);
                    })
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }

            return host;
        }
    }
}
