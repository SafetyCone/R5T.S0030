using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0048;
using R5T.D0078;
using R5T.D0079;
using R5T.D0083;
using R5T.D0096;
using R5T.D0096.D003;
using R5T.D0101;
using R5T.D0105;
using R5T.L0017.D001;
using R5T.T0062;
using R5T.T0063;

using N004 = R5T.S0030.T003.N004;


namespace R5T.S0030
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O201_AddServiceImplementationMarkerAttributeAndInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O201_AddServiceImplementationMarkerAttributeAndInterface> AddO201_AddServiceImplementationMarkerAttributeAndInterfaceAction(this IServiceAction _,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            var serviceAction = _.New<O201_AddServiceImplementationMarkerAttributeAndInterface>(services => services.AddO201_AddServiceImplementationMarkerAttributeAndInterface(
                projectRepositoryAction,
                stringlyTypedPathOperatorAction,
                visualStudioProjectFileOperatorAction,
                visualStudioProjectFileReferencesProviderAction,
                visualStudioSolutionFileOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O200_AddServiceDefinitionMarkerAttributeAndInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O200_AddServiceDefinitionMarkerAttributeAndInterface> AddO200_AddServiceDefinitionMarkerAttributeAndInterfaceAction(this IServiceAction _,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            var serviceAction = _.New<O200_AddServiceDefinitionMarkerAttributeAndInterface>(services => services.AddO200_AddServiceDefinitionMarkerAttributeAndInterface(
                projectRepositoryAction,
                stringlyTypedPathOperatorAction,
                visualStudioProjectFileOperatorAction,
                visualStudioProjectFileReferencesProviderAction,
                visualStudioSolutionFileOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O106_OutputServiceAddMethodsForProject"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O106_OutputServiceAddMethodsForProject> AddO106_OutputServiceAddMethodsForProjectAction(this IServiceAction _,
            IServiceAction<N004.IClassContextProvider> classContextProviderAction_N004,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O106_OutputServiceAddMethodsForProject>(services => services.AddO106_OutputServiceAddMethodsForProject(
                classContextProviderAction_N004,
                notepadPlusPlusOperatorAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O105_AddServiceImplementationsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O105_AddServiceImplementationsToRepository> AddO105_AddServiceImplementationsToRepositoryAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction,
            IServiceAction<O105A_IdentifyServiceImplementations> o105A_IdentifyServiceImplementationsAction,
            IServiceAction<O105B_AddServiceImplementationsToRepository> o105B_AddServiceImplementationsToRepositoryAction)
        {
            var serviceAction = _.New<O105_AddServiceImplementationsToRepository>(services => services.AddO105_AddServiceImplementationsToRepository(
                loggerUnboundAction,
                projectFilePathsProviderAction,
                serviceRepositoryAction,
                o105A_IdentifyServiceImplementationsAction,
                o105B_AddServiceImplementationsToRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O105B_AddServiceImplementationsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O105B_AddServiceImplementationsToRepository> AddO105B_AddServiceImplementationsToRepositoryAction(this IServiceAction _,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O105B_AddServiceImplementationsToRepository>(services => services.AddO105B_AddServiceImplementationsToRepository(
                humanOutputAction,
                loggerUnboundAction,
                projectRepositoryAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O105A_IdentifyServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O105A_IdentifyServiceImplementations> AddO105A_IdentifyServiceImplementationsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction)
        {
            var serviceAction = _.New<O105A_IdentifyServiceImplementations>(services => services.AddO105A_IdentifyServiceImplementations(
                loggerUnboundAction,
                serviceImplementationCodeFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O104_AddDependencyDefinitionsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O104_AddDependencyDefinitionsToRepository> AddO104_AddDependencyDefinitionsToRepositoryAction(this IServiceAction _,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O104_AddDependencyDefinitionsToRepository>(services => services.AddO104_AddDependencyDefinitionsToRepository(
                humanOutputAction,
                humanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                outputFilePathProviderAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O103_AddImplementedDefinitionToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O103_AddImplementedDefinitionToRepository> AddO103_AddImplementedDefinitionToRepositoryAction(this IServiceAction _,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O103_AddImplementedDefinitionToRepository>(services => services.AddO103_AddImplementedDefinitionToRepository(
                humanOutputAction,
                humanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                outputFilePathProviderAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O102_AddServiceImplementationsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O102_AddServiceImplementationsToRepository> AddO102_AddServiceImplementationsToRepositoryAction(this IServiceAction _,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction,
            IServiceAction<O003_IdentifyServiceImplementationsCore> o003_IdentifyServiceImplementationsCoreAction)
        {
            var serviceAction = _.New<O102_AddServiceImplementationsToRepository>(services => services.AddO102_AddServiceImplementationsToRepository(
                humanOutputAction,
                humanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                projectRepositoryAction,
                serviceRepositoryAction,
                o003_IdentifyServiceImplementationsCoreAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O101_AddServiceDefinitionsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O101_AddServiceDefinitionsToRepository> AddO101_AddServiceDefinitionsToRepositoryAction(this IServiceAction _,
            IServiceAction<IExecutableDirectoryFilePathProvider> executableDirectoryFilePathProviderAction,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction,
            IServiceAction<O001_IdentifyServiceDefinitionsCore> o001_IdentifyServiceDefinitionsCoreAction)
        {
            var serviceAction = _.New<O101_AddServiceDefinitionsToRepository>(services => services.AddO101_AddServiceDefinitionsToRepository(
                executableDirectoryFilePathProviderAction,
                humanOutputAction,
                humanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                projectRepositoryAction,
                serviceRepositoryAction,
                o001_IdentifyServiceDefinitionsCoreAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O100_PerformAllSurveys"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O100_PerformAllSurveys> AddO100_PerformAllSurveysAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceDefinitionCodeFilePathsProvider> serviceDefinitionCodeFilePathsProviderAction,
            IServiceAction<IServiceDefinitionTypeIdentifier> serviceDefinitionTypeIdentifierAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<IServiceImplementationTypeIdentifier> serviceImplementationTypeIdentifierAction,
            IServiceAction<O001A_DescribeServiceComponents> o001A_DescribeServiceComponentsAction,
            IServiceAction<O002A_DescribePossibleServiceComponents> o002A_DescribePossibleServiceComponentsAction)
        {
            var serviceAction = _.New<O100_PerformAllSurveys>(services => services.AddO100_PerformAllSurveys(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                serviceDefinitionCodeFilePathsProviderAction,
                serviceDefinitionTypeIdentifierAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceImplementationTypeIdentifierAction,
                o001A_DescribeServiceComponentsAction,
                o002A_DescribePossibleServiceComponentsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O010_DescribeAllServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O010_DescribeAllServiceImplementations> AddO010_DescribeAllServiceImplementationsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O010_DescribeAllServiceImplementations>(services => services.AddO010_DescribeAllServiceImplementations(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O009a_DescribeAllServiceImplementationsInProject"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O009a_DescribeAllServiceImplementationsInProject> AddO009a_DescribeAllServiceImplementationsInProjectAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O009a_DescribeAllServiceImplementationsInProject>(services => services.AddO009a_DescribeAllServiceImplementationsInProject(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O009_DescribeServiceImplementationsInFile"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O009_DescribeServiceImplementationsInFile> AddO009_DescribeServiceImplementationsInFileAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O009_DescribeServiceImplementationsInFile>(services => services.AddO009_DescribeServiceImplementationsInFile(
                notepadPlusPlusOperatorAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O008_DescribeSingleServiceImplementation"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O008_DescribeSingleServiceImplementation> AddO008_DescribeSingleServiceImplementationAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O008_DescribeSingleServiceImplementation>(services => services.AddO008_DescribeSingleServiceImplementation(
                notepadPlusPlusOperatorAction,
                serviceRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O007_IdentifyServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O007_IdentifyServiceImplementations> AddO007_IdentifyServiceImplementationsAction(this IServiceAction _,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceImplementationCandidateIdentifier> serviceImplementationCandidateIdentifierAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            var serviceAction = _.New<O007_IdentifyServiceImplementations>(services => services.AddO007_IdentifyServiceImplementations(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                serviceImplementationCandidateIdentifierAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction));

            return serviceAction;
        }

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
        /// Adds the <see cref="O900_SortExternalServiceComponentDataFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O900_SortExternalServiceComponentDataFiles> AddO900_SortExternalServiceComponentDataFilesAction(this IServiceAction _)
        {
            var serviceAction = _.New<O900_SortExternalServiceComponentDataFiles>(services => services.AddO900_SortExternalServiceComponentDataFiles());

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