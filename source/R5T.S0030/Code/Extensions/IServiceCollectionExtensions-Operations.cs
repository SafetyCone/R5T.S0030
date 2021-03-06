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
using R5T.T0063;

using N004 = R5T.S0030.T003.N004;


namespace R5T.S0030
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O201_AddServiceImplementationMarkerAttributeAndInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO201_AddServiceImplementationMarkerAttributeAndInterface(this IServiceCollection services,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            services
                .Run(projectRepositoryAction)
                .Run(stringlyTypedPathOperatorAction)
                .Run(visualStudioProjectFileOperatorAction)
                .Run(visualStudioProjectFileReferencesProviderAction)
                .Run(visualStudioSolutionFileOperatorAction)
                .AddSingleton<O201_AddServiceImplementationMarkerAttributeAndInterface>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O200_AddServiceDefinitionMarkerAttributeAndInterface"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO200_AddServiceDefinitionMarkerAttributeAndInterface(this IServiceCollection services,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            services
                .Run(projectRepositoryAction)
                .Run(stringlyTypedPathOperatorAction)
                .Run(visualStudioProjectFileOperatorAction)
                .Run(visualStudioProjectFileReferencesProviderAction)
                .Run(visualStudioSolutionFileOperatorAction)
                .AddSingleton<O200_AddServiceDefinitionMarkerAttributeAndInterface>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O106_OutputServiceAddMethodsForProject"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO106_OutputServiceAddMethodsForProject(this IServiceCollection services,
            IServiceAction<N004.IClassContextProvider> classContextProviderAction_N004,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(classContextProviderAction_N004)
                .Run(notepadPlusPlusOperatorAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O106_OutputServiceAddMethodsForProject>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O105_AddServiceImplementationsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO105_AddServiceImplementationsToRepository(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction,
            IServiceAction<O105A_IdentifyServiceImplementations> o105A_IdentifyServiceImplementationsAction,
            IServiceAction<O105B_AddServiceImplementationsToRepository> o105B_AddServiceImplementationsToRepositoryAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceRepositoryAction)
                .Run(o105A_IdentifyServiceImplementationsAction)
                .Run(o105B_AddServiceImplementationsToRepositoryAction)
                .AddSingleton<O105_AddServiceImplementationsToRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O105B_AddServiceImplementationsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO105B_AddServiceImplementationsToRepository(this IServiceCollection services,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(humanOutputAction)
                .Run(loggerUnboundAction)
                .Run(projectRepositoryAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O105B_AddServiceImplementationsToRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O105A_IdentifyServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO105A_IdentifyServiceImplementations(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .AddSingleton<O105A_IdentifyServiceImplementations>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O104_AddDependencyDefinitionsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO104_AddDependencyDefinitionsToRepository(this IServiceCollection services,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(humanOutputAction)
                .Run(humanOutputFilePathProviderAction)
                .Run(loggerUnboundAction)
                .Run(mainFileContextFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(outputFilePathProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O104_AddDependencyDefinitionsToRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O103_AddImplementedDefinitionToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO103_AddImplementedDefinitionToRepository(this IServiceCollection services,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(humanOutputAction)
                .Run(humanOutputFilePathProviderAction)
                .Run(loggerUnboundAction)
                .Run(mainFileContextFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(outputFilePathProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O103_AddImplementedDefinitionToRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O102_AddServiceImplementationsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO102_AddServiceImplementationsToRepository(this IServiceCollection services,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IHumanOutputFilePathProvider> humanOutputFilePathProviderAction,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<IMainFileContextFilePathsProvider> mainFileContextFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction,
            IServiceAction<O003_IdentifyServiceImplementationsCore> o003_IdentifyServiceImplementationsCoreAction)
        {
            services
                .Run(humanOutputAction)
                .Run(humanOutputFilePathProviderAction)
                .Run(loggerUnboundAction)
                .Run(mainFileContextFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectRepositoryAction)
                .Run(serviceRepositoryAction)
                .Run(o003_IdentifyServiceImplementationsCoreAction)
                .AddSingleton<O102_AddServiceImplementationsToRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O101_AddServiceDefinitionsToRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO101_AddServiceDefinitionsToRepository(this IServiceCollection services,
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
            services
                .Run(executableDirectoryFilePathProviderAction)
                .Run(humanOutputAction)
                .Run(humanOutputFilePathProviderAction)
                .Run(loggerUnboundAction)
                .Run(mainFileContextFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectRepositoryAction)
                .Run(serviceRepositoryAction)
                .Run(o001_IdentifyServiceDefinitionsCoreAction)
                .AddSingleton<O101_AddServiceDefinitionsToRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O100_PerformAllSurveys"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO100_PerformAllSurveys(this IServiceCollection services,
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
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceDefinitionCodeFilePathsProviderAction)
                .Run(serviceDefinitionTypeIdentifierAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceImplementationTypeIdentifierAction)
                .Run(o001A_DescribeServiceComponentsAction)
                .Run(o002A_DescribePossibleServiceComponentsAction)
                .AddSingleton<O100_PerformAllSurveys>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O010_DescribeAllServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO010_DescribeAllServiceImplementations(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O010_DescribeAllServiceImplementations>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O009a_DescribeAllServiceImplementationsInProject"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO009a_DescribeAllServiceImplementationsInProject(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O009a_DescribeAllServiceImplementationsInProject>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O009_DescribeServiceImplementationsInFile"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO009_DescribeServiceImplementationsInFile(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O009_DescribeServiceImplementationsInFile>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O008_DescribeSingleServiceImplementation"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO008_DescribeSingleServiceImplementation(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O008_DescribeSingleServiceImplementation>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O007_IdentifyServiceImplementations"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO007_IdentifyServiceImplementations(this IServiceCollection services,
            IServiceAction<ILoggerUnbound> loggerUnboundAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectFilePathsProvider> projectFilePathsProviderAction,
            IServiceAction<IServiceImplementationCandidateIdentifier> serviceImplementationCandidateIdentifierAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(loggerUnboundAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectFilePathsProviderAction)
                .Run(serviceImplementationCandidateIdentifierAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<O007_IdentifyServiceImplementations>();

            return services;
        }

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
        /// Adds the <see cref="O900_SortExternalServiceComponentDataFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO900_SortExternalServiceComponentDataFiles(this IServiceCollection services)
        {
            services.AddSingleton<O900_SortExternalServiceComponentDataFiles>();

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