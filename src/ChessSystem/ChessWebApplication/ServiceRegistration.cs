namespace ChessWebApplication
{
    using ChessSystem.Application;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddConventionalServices(typeof(ServiceRegistration).Assembly);

            services.AddScoped<BrowserService>();

            return services;
        }
    }
}
