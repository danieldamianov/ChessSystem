namespace ChessWebApplicationStartUp
{
    using ChessSystem.Application;
    using ChessSystem.Application.Common.Interfaces;
    using ChessWebApplication;
    using ChessWebApplication.Controllers;
    using ChessWebApplication.Hubs;
    using ChessWebApplication.Hubs.OnlineUsers;
    using ChessWebApplication.Middlewares;
    using ChessWebApplication.Services;
    using FluentValidation.AspNetCore;
    using Infrastructure;
    using Infrastructure.Persistence;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApplication()
                .AddInfrastructure(this.Configuration)
                .AddWebComponents();

            services
                .AddHealthChecks()
                .AddDbContextCheck<ChessApplicationDbContext>();

            services
                .AddControllers()
                .AddFluentValidation(options => options
                    .RegisterValidatorsFromAssemblyContaining<IChessApplicationData>())
                .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var assembly = typeof(HomeController).Assembly;
            services.AddControllersWithViews()
                .AddApplicationPart(assembly);
            services.AddRazorPages();

            services.AddServerSideBlazor();
            services.AddSignalR();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseCustomExceptionHandler();

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapHub<OnlineUsersHub>("/OnlineUsers");
            });
        }
    }
}
