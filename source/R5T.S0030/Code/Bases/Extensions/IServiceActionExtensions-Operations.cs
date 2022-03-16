using System;
using Microsoft.Extensions.DependencyInjection;

using R5T.D0096;
using R5T.D0105;
using R5T.L0017.D001;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O006_IdentityServiceImplementationsLackingMarkerInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O006_IdentityServiceImplementationsLackingMarkerInterface> AddO006_IdentityServiceImplementationsLackingMarkerInterfaceAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction)
        {
            var serviceAction = _.New<O006_IdentityServiceImplementationsLackingMarkerInterface>(services => services.AddO006_IdentityServiceImplementationsLackingMarkerInterface(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                o001A_DescribeServiceComponentsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O005_IdentityServiceDefinitionsLackingMarkerInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O005_IdentityServiceDefinitionsLackingMarkerInterface> AddO005_IdentityServiceDefinitionsLackingMarkerInterfaceAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction)
        {
            var serviceAction = _.New<O005_IdentityServiceDefinitionsLackingMarkerInterface>(services => services.AddO005_IdentityServiceDefinitionsLackingMarkerInterface(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                o001A_DescribeServiceComponentsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O004_IdentifyPossibleServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O004_IdentifyPossibleServiceImplementations> AddO004_IdentifyPossibleServiceImplementationsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O002A_DescribePossibleServiceComponents> o002A_DescribePossibleServiceComponentsAction,
            IServiceAction<O004_IdentifyPossibleServiceImplementationsCore> o004_IdentifyPossibleServiceImplementationsCoreAction)
        {
            var serviceAction = _.New<O004_IdentifyPossibleServiceImplementations>(services => services.AddO004_IdentifyPossibleServiceImplementations(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001A_DescribeServiceComponentsAction,
                o002A_DescribePossibleServiceComponentsAction,
                o004_IdentifyPossibleServiceImplementationsCoreAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O004_IdentifyPossibleServiceImplementationsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O004_IdentifyPossibleServiceImplementationsCore> AddO004_IdentifyPossibleServiceImplementationsCoreAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction)
        {
            var serviceAction = _.New<O004_IdentifyPossibleServiceImplementationsCore>(services => services.AddO004_IdentifyPossibleServiceImplementationsCore(
                loggerUnboundAction,
                projectFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003_IdentifyServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003_IdentifyServiceImplementations> AddO003_IdentifyServiceImplementationsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O003_IdentifyServiceImplementationsCore> o003_IdentifyServiceImplementationsCoreAction)
        {
            var serviceAction = _.New<O003_IdentifyServiceImplementations>(services => services.AddO003_IdentifyServiceImplementations(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001A_DescribeServiceComponentsAction,
                o003_IdentifyServiceImplementationsCoreAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003_IdentifyServiceImplementationsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003_IdentifyServiceImplementationsCore> AddO003_IdentifyServiceImplementationsCoreAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<IServiceImplementationTypeIdentifier> serviceImplementationTypeIdentifierAction)
        {
            var serviceAction = _.New<O003_IdentifyServiceImplementationsCore>(services => services.AddO003_IdentifyServiceImplementationsCore(
                loggerUnboundAction,
                projectFilePathsProviderAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceImplementationTypeIdentifierAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O002_IdentifyPossibleServiceDefinitions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O002_IdentifyPossibleServiceDefinitions> AddO002_IdentifyPossibleServiceDefinitionsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O002A_DescribePossibleServiceComponents> o002A_DescribePossibleServiceComponentsAction,
            IServiceAction<O002_IdentifyPossibleServiceDefinitionsCore> o002_IdentifyPossibleServiceDefinitionsCoreAction)
        {
            var serviceAction = _.New<O002_IdentifyPossibleServiceDefinitions>(services => services.AddO002_IdentifyPossibleServiceDefinitions(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001A_DescribeServiceComponentsAction,
                o002A_DescribePossibleServiceComponentsAction,
                o002_IdentifyPossibleServiceDefinitionsCoreAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O002_IdentifyPossibleServiceDefinitionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O002_IdentifyPossibleServiceDefinitionsCore> AddO002_IdentifyPossibleServiceDefinitionsCoreAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction)
        {
            var serviceAction = _.New<O002_IdentifyPossibleServiceDefinitionsCore>(services => services.AddO002_IdentifyPossibleServiceDefinitionsCore(
                loggerUnboundAction,
                projectFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O002A_DescribePossibleServiceComponents"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O002A_DescribePossibleServiceComponents> AddO002A_DescribePossibleServiceComponentsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction)
        {
            var serviceAction = _.New<O002A_DescribePossibleServiceComponents>(services => services.AddO002A_DescribePossibleServiceComponents(
                loggerUnboundAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001_IdentifyServiceDefinitions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001_IdentifyServiceDefinitions> AddO001_IdentityServiceDefinitionsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<O001_IdentifyServiceDefinitionsCore> o001_IdentityServiceDefinitionsCoreAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceDefinitionsAction)
        {
            var serviceAction = _.New<O001_IdentifyServiceDefinitions>(services => services.AddO001_IdentityServiceDefinitions(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001_IdentityServiceDefinitionsCoreAction,
                o001A_DescribeServiceDefinitionsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001A_DescribeServiceComponents"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001A_DescribeServiceComponents> AddO001A_DescribeServiceComponentsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction)
        {
            var serviceAction = _.New<O001A_DescribeServiceComponents>(services => services.AddO001A_DescribeServiceComponents(
                loggerUnboundAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001_IdentifyServiceDefinitionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001_IdentifyServiceDefinitionsCore> AddO001_IdentityServiceDefinitionsCoreAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceDefinitionCodeFilePathsProvider> serviceDefinitionCodeFilePathsProviderAction,
            IServiceAction<IServiceDefinitionTypeIdentifier> serviceDefinitionDescriptorProviderAction)
        {
            var serviceAction = _.New<O001_IdentifyServiceDefinitionsCore>(services => services.AddO001_IdentityServiceDefinitionsCore(
                loggerAction,
                projectFilePathsProviderAction,
                serviceDefinitionCodeFilePathsProviderAction,
                serviceDefinitionDescriptorProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O999_Scratch"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O999_Scratch> AddO999_ScratchAction(this IServiceAction _,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<ILoggerUnbound> loggerAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceDefinitionCodeFilePathsProvider> serviceDefinitionCodeFilePathsProviderAction,
            IServiceAction<IServiceDefinitionTypeIdentifier> serviceDefinitionDescriptorProviderAction)
        {
            var serviceAction = _.New<O999_Scratch>(services => services.AddO999_Scratch(
                humanOutputAction,
                loggerAction,
                projectFilePathsProviderAction,
                serviceDefinitionCodeFilePathsProviderAction,
                serviceDefinitionDescriptorProviderAction));

            return serviceAction;
        }
    }
}