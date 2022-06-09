using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;


namespace R5T.S0030.T003.N012
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="InterfaceContextProvider"/> implementation of <see cref="IInterfaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddInterfaceContextProvider(this IServiceCollection services)
        {
            services.AddSingleton<IInterfaceContextProvider, InterfaceContextProvider>();

            return services;
        }
    }
}
