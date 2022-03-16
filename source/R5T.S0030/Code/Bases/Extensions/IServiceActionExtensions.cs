using System;
using Microsoft.Extensions.DependencyInjection;

using R5T.D0101;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ServiceImplementationTypeIdentifier"/> implementation of <see cref="IServiceImplementationTypeIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceImplementationTypeIdentifier> AddServiceImplementationTypeIdentifierAction(this IServiceAction _)
        {
            var serviceAction = _.New<IServiceImplementationTypeIdentifier>(services => services.AddServiceImplementationTypeIdentifier());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceImplementationCodeFilePathsProvider"/> implementation of <see cref="IServiceImplementationCodeFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceImplementationCodeFilePathsProvider> AddServiceImplementationCodeFilePathsProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IServiceImplementationCodeFilePathsProvider>(services => services.AddServiceImplementationCodeFilePathsProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceDefinitionTypeIdentifier"/> implementation of <see cref="IServiceDefinitionTypeIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceDefinitionTypeIdentifier> AddServiceDefinitionTypeIdentifierAction(this IServiceAction _)
        {
            var serviceAction = _.New<IServiceDefinitionTypeIdentifier>(services => services.AddServiceDefinitionTypeIdentifier());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceDefinitionCodeFilePathsProvider"/> implementation of <see cref="IServiceDefinitionCodeFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceDefinitionCodeFilePathsProvider> AddServiceDefinitionCodeFilePathsProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IServiceDefinitionCodeFilePathsProvider>(services => services.AddServiceDefinitionCodeFilePathsProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ProjectFilePathsProvider"/> implementation of <see cref="IProjectFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProjectFilePathsProvider> AddProjectFilePathsProviderAction(this IServiceAction _,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<IProjectFilePathsProvider>(services => services.AddProjectFilePathsProvider(
                projectRepositoryAction));

            return serviceAction;
        }

        public static IServiceAction<HostStartup> AddHostStartupAction(this IServiceAction _)
        {
            var output = _.New<HostStartup>(services => services.AddHostStartup());
                
            return output;
        }
    }
}