using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0065;
using R5T.D0088.I0002;
using R5T.D0101;
using R5T.T0063;
using R5T.T0128;
using R5T.T0128.D001;

using R5T.S0030.FileContexts;
using R5T.S0030.Repositories;


namespace R5T.S0030
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="MainFileContextProvider"/> implementation of <see cref="IFileContextProvider{MainFileContext}"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddMainFileContextProvider(this IServiceCollection services,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction)
        {
            services
                .Run(mainFileContextFilePathsProviderAction)
                .AddSingleton<IFileContextProvider<MainFileContext>, MainFileContextProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceRepository{TFileContext}"/> implementation of <see cref="IServiceRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceRepository<TFileContext>(this IServiceCollection services,
            IServiceAction<IFileContextProvider<TFileContext>> fileContextProviderAction)
            where TFileContext : FileContext, FileContexts.IServiceRepositoryFileContext
        {
            services
                .Run(fileContextProviderAction)
                .AddSingleton<IServiceRepository, ServiceRepository<TFileContext>>();

            return services;
        }

        public static IServiceCollection AddHostStartup(this IServiceCollection services)
        {           
            var dependencyServiceActions = new DependencyServiceActionAggregation();
                
            services.AddHostStartup<HostStartup>(dependencyServiceActions)
                // Add services required by HostStartup, but not by HostStartupBase.
                ;
                
            return services;
        }
    }
}