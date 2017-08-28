using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Tracking
{
    public static class TrackingExtensions
    {

        public static IServiceCollection AddTimeTracking(this IServiceCollection services)
        {
            services.TryAddSingleton<ITimestamper, Timestamper>();
            services.TryAddSingleton<IModifiedResolver, ModifiedResolver>();
            services.TryAddSingleton<ICreatedResolver, CreatedResolver>();

            return services;
        }
    }
}
