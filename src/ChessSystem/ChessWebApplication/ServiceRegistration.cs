﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWebApplication
{
    using ChessSystem.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddConventionalServices(typeof(ServiceRegistration).Assembly);

            return services;
        }
    }
}