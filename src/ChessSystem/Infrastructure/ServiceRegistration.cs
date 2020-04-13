namespace Infrastructure
{
    using ChessSystem.Application;
    using ChessSystem.Application.Common.Interfaces;
    using Identity;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Services;

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
                .AddIdentityServer()
                .AddApiAuthorization<ChessAppUser, ChessApplicationDbContext>();

            services
                .AddConventionalServices(typeof(ServiceRegistration).Assembly);

            // services
            //    .AddTransient<IDateTime, DateTimeService>()
            //    .AddTransient<IIdentity, IdentityService>();

            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
