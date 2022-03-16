using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0088.I0002;
using R5T.D0101;
using R5T.T0063;


namespace R5T.S0030
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ServiceImplementationTypeIdentifier"/> implementation of <see cref="IServiceImplementationTypeIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceImplementationTypeIdentifier(this IServiceCollection services)
        {
            services.AddSingleton<IServiceImplementationTypeIdentifier, ServiceImplementationTypeIdentifier>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceImplementationCodeFilePathsProvider"/> implementation of <see cref="IServiceImplementationCodeFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceImplementationCodeFilePathsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IServiceImplementationCodeFilePathsProvider, ServiceImplementationCodeFilePathsProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceDefinitionTypeIdentifier"/> implementation of <see cref="IServiceDefinitionTypeIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceDefinitionTypeIdentifier(this IServiceCollection services)
        {
            services.AddSingleton<IServiceDefinitionTypeIdentifier, ServiceDefinitionTypeIdentifier>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceDefinitionCodeFilePathsProvider"/> implementation of <see cref="IServiceDefinitionCodeFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceDefinitionCodeFilePathsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IServiceDefinitionCodeFilePathsProvider, ServiceDefinitionCodeFilePathsProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ProjectFilePathsProvider"/> implementation of <see cref="IProjectFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddProjectFilePathsProvider(this IServiceCollection services,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(projectRepositoryAction)
                .AddSingleton<IProjectFilePathsProvider, ProjectFilePathsProvider>();

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