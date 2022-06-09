using System;

using R5T.D0084.D002;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N007
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="LocalRepositoryContextProvider"/> implementation of <see cref="ILocalRepositoryContextProvider"/> as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ILocalRepositoryContextProvider> AddLocalRepositoryContextProviderAction(this IServiceAction _,
            IServiceAction<N006.ILocalRepositoryContextProvider> localRepositoryContextProviderAction,
            IServiceAction<N005.IRemoteRepositoryContextProvider> remoteRepositoryContextProviderAction,
            IServiceAction<IRepositoriesDirectoryPathProvider> repositoriesDirectoryPathProviderAction)
        {
            var serviceAction = _.New<ILocalRepositoryContextProvider>(services => services.AddLocalRepositoryContextProvider(
                localRepositoryContextProviderAction,
                remoteRepositoryContextProviderAction,
                repositoriesDirectoryPathProviderAction));

            return serviceAction;
        }
    }
}
