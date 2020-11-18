using Microsoft.Extensions.DependencyInjection;
using System;

namespace Yugen.Toolkit.Standard.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransientFactory<TImplementation>(this IServiceCollection services)
            where TImplementation : class
        {
            services.AddTransient<TImplementation>();
            services.AddSingleton<Func<TImplementation>>(x => () => x.GetService<TImplementation>());
            return services;
        }

        public static IServiceCollection AddSingletonFactory<TImplementation>(this IServiceCollection services)
            where TImplementation : class
        {
            services.AddSingleton<TImplementation>();
            services.AddSingleton<Func<TImplementation>>(x => () => x.GetService<TImplementation>());
            return services;
        }
    }
}