using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0037;
using R5T.T0063;


namespace R5T.S0030.T003.N006
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="LocalRepositoryContextProvider"/> implementation of <see cref="ILocalRepositoryContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddLocalRepositoryContextProvider(this IServiceCollection services,
            IServiceAction<IGitOperator> gitOperatorAction)
        {
            services
                .Run(gitOperatorAction)
                .AddSingleton<ILocalRepositoryContextProvider, LocalRepositoryContextProvider>();

            return services;
        }
    }
}
