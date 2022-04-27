using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0116;
using R5T.T0063;


namespace R5T.S0030.T003.N002
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="NamespaceContextProvider"/> implementation of <see cref="INamespaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddNamespaceContextProvider(this IServiceCollection services,
            IServiceAction<IUsingDirectivesFormatter> usingDirectivesFormatterAction)
        {
            services
                .Run(usingDirectivesFormatterAction)
                .AddSingleton<INamespaceContextProvider, NamespaceContextProvider>();

            return services;
        }
    }
}
