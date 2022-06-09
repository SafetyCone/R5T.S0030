using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0084.D002;
using R5T.T0063;


namespace R5T.S0030.T003.N007
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="LocalRepositoryContextProvider"/> implementation of <see cref="ILocalRepositoryContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddLocalRepositoryContextProvider(this IServiceCollection services,
            IServiceAction<N006.ILocalRepositoryContextProvider> localRepositoryContextProviderAction,
            IServiceAction<N005.IRemoteRepositoryContextProvider> remoteRepositoryContextProviderAction,
            IServiceAction<IRepositoriesDirectoryPathProvider> repositoriesDirectoryPathProviderAction)
        {
            services
                .Run(localRepositoryContextProviderAction)
                .Run(remoteRepositoryContextProviderAction)
                .Run(repositoriesDirectoryPathProviderAction)
                .AddSingleton<ILocalRepositoryContextProvider, LocalRepositoryContextProvider>();

            return services;
        }
    }
}
