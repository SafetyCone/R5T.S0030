using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0065;
using R5T.D0101;
using R5T.T0062;
using R5T.T0063;
using R5T.T0128;
using R5T.T0128.D001;

using R5T.S0030.FileContexts;
using R5T.S0030.Repositories;


namespace R5T.S0030
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="MainFileContextProvider"/> implementation of <see cref="IFileContextProvider{MainFileContext}"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IFileContextProvider<MainFileContext>> AddMainFileContextProviderAction(this IServiceAction _,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction)
        {
            var serviceAction = _.New<IFileContextProvider<MainFileContext>>(services => services.AddMainFileContextProvider(
                mainFileContextFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceRepository{TFileContext}"/> implementation of <see cref="IServiceRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceRepository> AddServiceRepositoryAction<TFileContext>(this IServiceAction _,
            IServiceAction<IFileContextProvider<TFileContext>> fileContextProviderAction)
            where TFileContext : FileContext, FileContexts.IServiceRepositoryFileContext
        {
            var serviceAction = _.New<IServiceRepository>(services => services.AddServiceRepository(
                fileContextProviderAction));

            return serviceAction;
        }

        public static IServiceAction<HostStartup> AddHostStartupAction(this IServiceAction _)
        {
            var output = _.New<HostStartup>(services => services.AddHostStartup());
                
            return output;
        }
    }
}