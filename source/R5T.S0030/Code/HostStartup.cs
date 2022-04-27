using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Magyar;
using R5T.Ostrogothia.Rivet;

using R5T.A0003;
using R5T.D0048.Default;
using R5T.D0077.A002;
using R5T.D0078.A002;
using R5T.D0079.A002;
using R5T.D0081.I001;
using R5T.D0083.I001;
using R5T.D0088.I0002;
using R5T.D0094.I001;
using R5T.D0095.I001;
using R5T.D0101.I0001;
using R5T.D0101.I001;
using R5T.D0105.I001;
using R5T.D0116.A0001;
using R5T.T0063;

using R5T.S0030.FileContexts;

using IProvidedServiceActionAggregation = R5T.D0088.I0002.IProvidedServiceActionAggregation;
using IRequiredServiceActionAggregation = R5T.D0088.I0002.IRequiredServiceActionAggregation;
using ServicesPlatformRequiredServiceActionAggregation = R5T.A0003.RequiredServiceActionAggregation;


namespace R5T.S0030
{
    public class HostStartup : HostStartupBase
    {
        public override Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
        {
            // Do nothing.
        
            return Task.CompletedTask;
        }

        protected override Task ConfigureServices(IServiceCollection services, IProvidedServiceActionAggregation providedServicesAggregation)
        {
            var loggerUnboundAction = providedServicesAggregation.LoggerAction;

            // Inputs.
            var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);

            var organizationProviderAction = Instances.ServiceAction.AddOrganizationProviderAction(); // Rivet organization.

            var rootOutputDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRootOutputDirectoryPathProviderAction(@"C:\Temp\Output");

            // Services platform.
            var servicesPlatformRequiredServiceActionAggregation = new ServicesPlatformRequiredServiceActionAggregation
            {
                ConfigurationAction = providedServicesAggregation.ConfigurationAction,
                ExecutionSynchronicityProviderAction = executionSynchronicityProviderAction,
                LoggerAction = loggerUnboundAction,
                LoggerFactoryAction = providedServicesAggregation.LoggerFactoryAction,
                MachineMessageOutputSinkProviderActions = EnumerableHelper.Empty<IServiceAction<D0099.D002.IMachineMessageOutputSinkProvider>>(),
                MachineMessageTypeJsonSerializationHandlerActions = EnumerableHelper.Empty<IServiceAction<D0098.IMachineMessageTypeJsonSerializationHandler>>(),
                OrganizationProviderAction = organizationProviderAction,
                RootOutputDirectoryPathProviderAction = rootOutputDirectoryPathProviderAction,
            };

            var servicesPlatform = Instances.ServiceAction.AddProvidedServiceActionAggregation(
                servicesPlatformRequiredServiceActionAggregation);

            // Logging.
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .SetMinimumLevel(LogLevel.Debug)
                    .AddConsole(
                        servicesPlatform.LoggerSynchronicityProviderAction)
                    .AddFile(
                        servicesPlatform.LogFilePathProviderAction,
                        servicesPlatform.LoggerSynchronicityProviderAction)
                    ;
            });

            // Notepad++
            var notepadPlusPlusExecutableFilePathProviderAction = Instances.ServiceAction.AddHardCodedNotepadPlusPlusExecutableFilePathProviderAction();

            var notepadPlusPlusOperatorAction = Instances.ServiceAction.AddNotepadPlusPlusOperatorAction(
                //commandLineOperatorAction,
                servicesPlatform.BaseCommandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction);

            // Project repository.
            var projectRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedProjectRepositoryFilePathsProviderAction();

            var fileBasedProjectRepositoryAction = Instances.ServiceAction.AddFileBasedProjectRepositoryAction(
                projectRepositoryFilePathsProviderAction);

            var projectRepositoryAction = Instances.ServiceAction.ForwardFileBasedProjectRepositoryToProjectRepositoryAction(
                fileBasedProjectRepositoryAction);

            // Visual studio.
            var dotnetOperatorActions = Instances.ServiceAction.AddDotnetOperatorActions(
                servicesPlatform.CommandLineOperatorAction,
                servicesPlatform.SecretsDirectoryFilePathProviderAction);
            var visualStudioProjectFileOperatorActions = Instances.ServiceAction.AddVisualStudioProjectFileOperatorActions(
                dotnetOperatorActions.DotnetOperatorAction,
                servicesPlatform.FileNameOperatorAction,
                servicesPlatform.StringlyTypedPathOperatorAction);
            var visualStudioProjectFileReferencesProviderAction = Instances.ServiceAction.AddVisualStudioProjectFileReferencesProviderAction(
                servicesPlatform.StringlyTypedPathOperatorAction);
            var visualStudioSolutionFileOperatorActions = Instances.ServiceAction.AddVisualStudioSolutionFileOperatorActions(
                dotnetOperatorActions.DotnetOperatorAction,
                servicesPlatform.FileNameOperatorAction,
                servicesPlatform.StringlyTypedPathOperatorAction);

            // Using directives formatter.
            var usingDirectivesFormatterActions = Instances.ServiceAction.AddUsingDirectivesFormatterActions();

            // Project services.
            // Level 00.
            var mainFileContextFilePathsProviderAction = Instances.ServiceAction.AddHardCodedMainFileContextFilePathsProviderAction();
            var serviceDefinitionCodeFilePathsProvider = Instances.ServiceAction.AddServiceDefinitionCodeFilePathsProviderAction();
            var serviceDefinitionTypeIdentifierAction = Instances.ServiceAction.AddServiceDefinitionTypeIdentifierAction();
            var serviceImplementationCandidateIdentifierAction = Instances.ServiceAction.AddServiceImplementationCandidateIdentifierAction();
            var serviceImplementationCodeFilePathsProviderAction = Instances.ServiceAction.AddServiceImplementationCodeFilePathsProviderAction();
            var serviceImplementationTypeIdentifierAction = Instances.ServiceAction.AddServiceImplementationTypeIdentifierAction();

            // Level 01.
            var classContextProviderAction_N003 = Instances.ServiceAction.AddClassContextProviderAction_N003();
            var compilationUnitContextProviderAction_N001 = Instances.ServiceAction.AddCompilationUnitContextProviderAction_N001(
                usingDirectivesFormatterActions.UsingDirectivesFormatterAction);
            var executableDirectoryFilePathProviderAction = Instances.ServiceAction.AddExecutableDirectoryFilePathProviderAction(
                servicesPlatform.ExecutableDirectoryPathProviderAction,
                servicesPlatform.StringlyTypedPathOperatorAction);
            var mainFileContextProviderAction = Instances.ServiceAction.AddMainFileContextProviderAction(
                mainFileContextFilePathsProviderAction);
            var namespaceContextProviderAction_N002 = Instances.ServiceAction.AddNamespaceContextProviderAction_N002(
                usingDirectivesFormatterActions.UsingDirectivesFormatterAction);
            var projectFilePathsProviderAction = Instances.ServiceAction.AddProjectFilePathsProviderAction(
                projectRepositoryAction);

            // Level 02.
            var classContextProviderAction_N004 = Instances.ServiceAction.AddClassContextProviderAction_N004(
                classContextProviderAction_N003,
                compilationUnitContextProviderAction_N001,
                namespaceContextProviderAction_N002);
            var serviceRepositoryAction = Instances.ServiceAction.AddServiceRepositoryAction<MainFileContext>(
                mainFileContextProviderAction);

            /// Operations.

            // Project operations.
            // Level 00.
            var o001A_DescribeServiceComponentsAction = Instances.ServiceAction.AddO001A_DescribeServiceComponentsAction(
                loggerUnboundAction);
            var o001_IdentityServiceDefinitionsCoreAction = Instances.ServiceAction.AddO001_IdentityServiceDefinitionsCoreAction(
                loggerUnboundAction,
                projectFilePathsProviderAction,
                serviceDefinitionCodeFilePathsProvider,
                serviceDefinitionTypeIdentifierAction);
            var o002A_DescribePossibleServiceComponentsAction = Instances.ServiceAction.AddO002A_DescribePossibleServiceComponentsAction(
                loggerUnboundAction);
            var o002_IdentifyPossibleServiceDefinitionsCoreAction = Instances.ServiceAction.AddO002_IdentifyPossibleServiceDefinitionsCoreAction(
                loggerUnboundAction,
                projectFilePathsProviderAction);
            var o003_IdentifyServiceImplementationsCoreAction = Instances.ServiceAction.AddO003_IdentifyServiceImplementationsCoreAction(
                loggerUnboundAction,
                projectFilePathsProviderAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceImplementationTypeIdentifierAction);
            var o004_IdentifyPossibleServiceImplementationsCoreAction = Instances.ServiceAction.AddO004_IdentifyPossibleServiceImplementationsCoreAction(
                loggerUnboundAction,
                projectFilePathsProviderAction);
            var o007_IdentifyServiceImplementationsAction = Instances.ServiceAction.AddO007_IdentifyServiceImplementationsAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                serviceImplementationCandidateIdentifierAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction);
            var o008_DescribeSingleServiceImplementationAction = Instances.ServiceAction.AddO008_DescribeSingleServiceImplementationAction(
                notepadPlusPlusOperatorAction,
                serviceRepositoryAction);
            var o009_DescribeServiceImplementationsInFileAction = Instances.ServiceAction.AddO009_DescribeServiceImplementationsInFileAction(
                notepadPlusPlusOperatorAction,
                serviceRepositoryAction);
            var o009a_DescribeAllServiceImplementationsInProjectAction = Instances.ServiceAction.AddO009a_DescribeAllServiceImplementationsInProjectAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction);
            var o010_DescribeAllServiceImplementationsAction = Instances.ServiceAction.AddO010_DescribeAllServiceImplementationsAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction);
            var o103_AddImplementedDefinitionToRepositoryAction = Instances.ServiceAction.AddO103_AddImplementedDefinitionToRepositoryAction(
                servicesPlatform.HumanOutputAction,
                servicesPlatform.HumanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                servicesPlatform.OutputFilePathProviderAction,
                serviceRepositoryAction);
            var o104_AddDependencyDefinitionsToRepositoryAction = Instances.ServiceAction.AddO104_AddDependencyDefinitionsToRepositoryAction(
                servicesPlatform.HumanOutputAction,
                servicesPlatform.HumanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                servicesPlatform.OutputFilePathProviderAction,
                serviceRepositoryAction);
            var o105A_IdentifyServiceImplementationsAction = Instances.ServiceAction.AddO105A_IdentifyServiceImplementationsAction(
                loggerUnboundAction,
                serviceImplementationCodeFilePathsProviderAction);
            var o105B_AddServiceImplementationsToRepositoryAction = Instances.ServiceAction.AddO105B_AddServiceImplementationsToRepositoryAction(
                servicesPlatform.HumanOutputAction,
                loggerUnboundAction,
                projectRepositoryAction,
                serviceRepositoryAction);
            var o200_AddServiceDefinitionMarkerAttributeAndInterfaceAction = Instances.ServiceAction.AddO200_AddServiceDefinitionMarkerAttributeAndInterfaceAction(
                projectRepositoryAction,
                servicesPlatform.StringlyTypedPathOperatorAction,
                visualStudioProjectFileOperatorActions.VisualStudioProjectFileOperatorAction,
                visualStudioProjectFileReferencesProviderAction,
                visualStudioSolutionFileOperatorActions.VisualStudioSolutionFileOperatorAction);
            var o201_AddServiceImplementationMarkerAttributeAndInterfaceAction = Instances.ServiceAction.AddO201_AddServiceImplementationMarkerAttributeAndInterfaceAction(
                projectRepositoryAction,
                servicesPlatform.StringlyTypedPathOperatorAction,
                visualStudioProjectFileOperatorActions.VisualStudioProjectFileOperatorAction,
                visualStudioProjectFileReferencesProviderAction,
                visualStudioSolutionFileOperatorActions.VisualStudioSolutionFileOperatorAction);
            var o900_SortExternalServiceComponentDataFilesAction = Instances.ServiceAction.AddO900_SortExternalServiceComponentDataFilesAction();
            var o999_ScratchAction = Instances.ServiceAction.AddO999_ScratchAction(
                servicesPlatform.HumanOutputAction,
                loggerUnboundAction,
                projectFilePathsProviderAction,
                serviceDefinitionCodeFilePathsProvider,
                serviceDefinitionTypeIdentifierAction);

            // Level 01.
            var o001_IdentityServiceDefinitionsAction = Instances.ServiceAction.AddO001_IdentityServiceDefinitionsAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001_IdentityServiceDefinitionsCoreAction,
                o001A_DescribeServiceComponentsAction);
            var o002_IdentifyPossibleServiceDefinitionsAction = Instances.ServiceAction.AddO002_IdentifyPossibleServiceDefinitionsAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001A_DescribeServiceComponentsAction,
                o002A_DescribePossibleServiceComponentsAction,
                o002_IdentifyPossibleServiceDefinitionsCoreAction);
            var o003_IdentifyServiceImplementationsAction = Instances.ServiceAction.AddO003_IdentifyServiceImplementationsAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001A_DescribeServiceComponentsAction,
                o003_IdentifyServiceImplementationsCoreAction);
            var o004_IdentifyPossibleServiceImplementationsAction = Instances.ServiceAction.AddO004_IdentifyPossibleServiceImplementationsAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                o001A_DescribeServiceComponentsAction,
                o002A_DescribePossibleServiceComponentsAction,
                o004_IdentifyPossibleServiceImplementationsCoreAction);
            var o005_IdentityServiceDefinitionsLackingMarkerInterfaceAction = Instances.ServiceAction.AddO005_IdentityServiceDefinitionsLackingMarkerInterfaceAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                o001A_DescribeServiceComponentsAction);
            var o006_IdentityServiceImplementationsLackingMarkerInterfaceAction = Instances.ServiceAction.AddO006_IdentityServiceImplementationsLackingMarkerInterfaceAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                o001A_DescribeServiceComponentsAction);
            var o101_AddServiceDefinitionsToRepositoryAction = Instances.ServiceAction.AddO101_AddServiceDefinitionsToRepositoryAction(
                executableDirectoryFilePathProviderAction,
                servicesPlatform.HumanOutputAction,
                servicesPlatform.HumanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                projectRepositoryAction,
                serviceRepositoryAction,
                o001_IdentityServiceDefinitionsCoreAction);
            var o102_AddServiceImplementationsToRepositoryAction = Instances.ServiceAction.AddO102_AddServiceImplementationsToRepositoryAction(
                servicesPlatform.HumanOutputAction,
                servicesPlatform.HumanOutputFilePathProviderAction,
                loggerUnboundAction,
                mainFileContextFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                projectRepositoryAction,
                serviceRepositoryAction,
                o003_IdentifyServiceImplementationsCoreAction);
            var o105_AddServiceImplementationsToRepositoryAction = Instances.ServiceAction.AddO105_AddServiceImplementationsToRepositoryAction(
                loggerUnboundAction,
                projectFilePathsProviderAction,
                serviceRepositoryAction,
                o105A_IdentifyServiceImplementationsAction,
                o105B_AddServiceImplementationsToRepositoryAction);
            var o106_OutputServiceAddMethodsForProjectAction = Instances.ServiceAction.AddO106_OutputServiceAddMethodsForProjectAction(
                classContextProviderAction_N004,
                notepadPlusPlusOperatorAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction);

            // Level 02.
            var o100_PerformAllSurveysAction = Instances.ServiceAction.AddO100_PerformAllSurveysAction(
                loggerUnboundAction,
                notepadPlusPlusOperatorAction,
                projectFilePathsProviderAction,
                serviceDefinitionCodeFilePathsProvider,
                serviceDefinitionTypeIdentifierAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceImplementationTypeIdentifierAction,
                o001A_DescribeServiceComponentsAction,
                o002A_DescribePossibleServiceComponentsAction);

            /// Experiments.
            var e001_CodeElementCreationAction = Instances.ServiceAction.AddE001_CodeElementCreationAction(
                classContextProviderAction_N003,
                classContextProviderAction_N004,
                compilationUnitContextProviderAction_N001,
                namespaceContextProviderAction_N002,
                notepadPlusPlusOperatorAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction);

            // Run.
            services.MarkAsServiceCollectonConfigurationStatement()
                // Services.
                .Run(servicesPlatform.ConfigurationAuditSerializerAction)
                .Run(servicesPlatform.ServiceCollectionAuditSerializerAction)
                // Operations.
                .Run(o001_IdentityServiceDefinitionsAction)
                .Run(o002_IdentifyPossibleServiceDefinitionsAction)
                .Run(o003_IdentifyServiceImplementationsAction)
                .Run(o004_IdentifyPossibleServiceImplementationsAction)
                .Run(o005_IdentityServiceDefinitionsLackingMarkerInterfaceAction)
                .Run(o006_IdentityServiceImplementationsLackingMarkerInterfaceAction)
                .Run(o007_IdentifyServiceImplementationsAction)
                .Run(o008_DescribeSingleServiceImplementationAction)
                .Run(o009_DescribeServiceImplementationsInFileAction)
                .Run(o009a_DescribeAllServiceImplementationsInProjectAction)
                .Run(o010_DescribeAllServiceImplementationsAction)
                .Run(o100_PerformAllSurveysAction)
                .Run(o101_AddServiceDefinitionsToRepositoryAction)
                .Run(o102_AddServiceImplementationsToRepositoryAction)
                .Run(o103_AddImplementedDefinitionToRepositoryAction)
                .Run(o104_AddDependencyDefinitionsToRepositoryAction)
                .Run(o105_AddServiceImplementationsToRepositoryAction)
                .Run(o106_OutputServiceAddMethodsForProjectAction)
                .Run(o200_AddServiceDefinitionMarkerAttributeAndInterfaceAction)
                .Run(o201_AddServiceImplementationMarkerAttributeAndInterfaceAction)
                .Run(o900_SortExternalServiceComponentDataFilesAction)
                .Run(o999_ScratchAction)
                .Run(e001_CodeElementCreationAction)
                ;
            return Task.CompletedTask;
        }


        protected override Task FillRequiredServiceActions(IRequiredServiceActionAggregation requiredServiceActions)
        {
            // Do nothing since none are required.
        
            return Task.CompletedTask;
        }
    }
}