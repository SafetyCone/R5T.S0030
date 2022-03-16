using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0096;
using R5T.D0105;
using R5T.L0017.D001;
using R5T.T0063;


namespace R5T.S0030
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O006_IdentityServiceImplementationsLackingMarkerInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO006_IdentityServiceImplementationsLackingMarkerInterface(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectFilePathsProviderAction)
                .Run(projectFilePathsProviderAction)
                .Run(o001A_DescribeServiceComponentsAction)
                .AddSingleton<O006_IdentityServiceImplementationsLackingMarkerInterface>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O005_IdentityServiceDefinitionsLackingMarkerInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO005_IdentityServiceDefinitionsLackingMarkerInterface(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectFilePathsProviderAction)
                .Run(projectFilePathsProviderAction)
                .Run(o001A_DescribeServiceComponentsAction)
                .AddSingleton<O005_IdentityServiceDefinitionsLackingMarkerInterface>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O004_IdentifyPossibleServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO004_IdentifyPossibleServiceImplementations(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O002A_DescribePossibleServiceComponents> o002A_DescribePossibleServiceComponentsAction,
            IServiceAction<O004_IdentifyPossibleServiceImplementationsCore> o004_IdentifyPossibleServiceImplementationsCoreAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(o001A_DescribeServiceComponentsAction)
                .Run(o002A_DescribePossibleServiceComponentsAction)
                .Run(o004_IdentifyPossibleServiceImplementationsCoreAction)
                .AddSingleton<O004_IdentifyPossibleServiceImplementations>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O004_IdentifyPossibleServiceImplementationsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO004_IdentifyPossibleServiceImplementationsCore(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(projectFilePathsProviderAction)
                .AddSingleton<O004_IdentifyPossibleServiceImplementationsCore>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003_IdentifyServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003_IdentifyServiceImplementations(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O003_IdentifyServiceImplementationsCore> o003_IdentifyServiceImplementationsCoreAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(o001A_DescribeServiceComponentsAction)
                .Run(o003_IdentifyServiceImplementationsCoreAction)
                .AddSingleton<O003_IdentifyServiceImplementations>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003_IdentifyServiceImplementationsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003_IdentifyServiceImplementationsCore(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<IServiceImplementationTypeIdentifier> serviceImplementationTypeIdentifierAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceImplementationTypeIdentifierAction)
                .AddSingleton<O003_IdentifyServiceImplementationsCore>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O002_IdentifyPossibleServiceDefinitions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO002_IdentifyPossibleServiceDefinitions(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O002A_DescribePossibleServiceComponents> o002A_DescribePossibleServiceComponentsAction,
            IServiceAction<O002_IdentifyPossibleServiceDefinitionsCore> o002_IdentifyPossibleServiceDefinitionsCoreAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(o001A_DescribeServiceComponentsAction)
                .Run(o002A_DescribePossibleServiceComponentsAction)
                .Run(o002_IdentifyPossibleServiceDefinitionsCoreAction)
                .AddSingleton<O002_IdentifyPossibleServiceDefinitions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O002_IdentifyPossibleServiceDefinitionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO002_IdentifyPossibleServiceDefinitionsCore(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(projectFilePathsProviderAction)
                .AddSingleton<O002_IdentifyPossibleServiceDefinitionsCore>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O002A_DescribePossibleServiceComponents"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO002A_DescribePossibleServiceComponents(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction)
        {
            services
                .Run(loggerUnboundAction)
                .AddSingleton<O002A_DescribePossibleServiceComponents>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001_IdentifyServiceDefinitions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001_IdentityServiceDefinitions(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001_IdentifyServiceDefinitionsCore> o001_IdentityServiceDefinitionsCoreAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceDefinitionsAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(o001_IdentityServiceDefinitionsCoreAction)
                .Run(o001A_DescribeServiceDefinitionsAction)
                .AddSingleton<O001_IdentifyServiceDefinitions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001A_DescribeServiceComponents"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001A_DescribeServiceComponents(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction)
        {
            services
                .Run(loggerUnboundAction)
                .AddSingleton<O001A_DescribeServiceComponents>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001_IdentifyServiceDefinitionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001_IdentityServiceDefinitionsCore(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceDefinitionCodeFilePathsProvider> serviceDefinitionCodeFilePathsProviderAction,
            IServiceAction<IServiceDefinitionTypeIdentifier> serviceDefinitionDescriptorProviderAction)
        {
            services
                .Run(loggerAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceDefinitionCodeFilePathsProviderAction)
                .Run(serviceDefinitionDescriptorProviderAction)
                .AddSingleton<O001_IdentifyServiceDefinitionsCore>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O999_Scratch"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO999_Scratch(this IServiceCollection services,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<ILoggerUnbound> loggerAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceDefinitionCodeFilePathsProvider> serviceDefinitionCodeFilePathsProviderAction,
            IServiceAction<IServiceDefinitionTypeIdentifier> serviceDefinitionDescriptorProviderAction)
        {
            services
                .Run(humanOutputAction)
                .Run(loggerAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceDefinitionCodeFilePathsProviderAction)
                .Run(serviceDefinitionDescriptorProviderAction)
                .AddSingleton<O999_Scratch>();

            return services;
        }
    }
}