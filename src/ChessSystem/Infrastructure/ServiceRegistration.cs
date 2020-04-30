namespace Infrastructure
{
    using ChessSystem.Application;
    using ChessSystem.Application.Common.Interfaces;
    using Infrastructure.Identity;
    using Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<ChessApplicationDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ChessApplicationDbContext).Assembly.FullName)))
                .AddScoped<IChessApplicationData>(provider => provider.GetService<ChessApplicationDbContext>());

            services
                .AddDefaultIdentity<ChessAppUser>()
                .AddEntityFrameworkStores<ChessApplicationDbContext>();

            services
                .AddConventionalServices(typeof(ServiceRegistration).Assembly);

            return services;
        }
    }
}
