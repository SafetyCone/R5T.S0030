using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Magyar;
using R5T.Ostrogothia.Rivet;

using R5T.A0003;
using R5T.D0048.Default;
using R5T.D0081.I001;
using R5T.D0088.I0002;
using R5T.D0094.I001;
using R5T.D0095.I001;
using R5T.D0101.I0001;
using R5T.D0101.I001;
using R5T.D0105.I001;
using R5T.T0063;

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

            // Project repository.
            var projectRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedProjectRepositoryFilePathsProviderAction();

            var fileBasedProjectRepositoryAction = Instances.ServiceAction.AddFileBasedProjectRepositoryAction(
                projectRepositoryFilePathsProviderAction);

            var projectRepositoryAction = Instances.ServiceAction.ForwardFileBasedProjectRepositoryToProjectRepositoryAction(
                fileBasedProjectRepositoryAction);

            // Notepad++
            var notepadPlusPlusExecutableFilePathProviderAction = Instances.ServiceAction.AddHardCodedNotepadPlusPlusExecutableFilePathProviderAction();

            var notepadPlusPlusOperatorAction = Instances.ServiceAction.AddNotepadPlusPlusOperatorAction(
                //commandLineOperatorAction,
                servicesPlatform.BaseCommandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction);

            // Project services.
            // Level 00.
            var serviceDefinitionCodeFilePathsProvider = Instances.ServiceAction.AddServiceDefinitionCodeFilePathsProviderAction();
            var serviceDefinitionTypeIdentifierAction = Instances.ServiceAction.AddServiceDefinitionTypeIdentifierAction();
            var serviceImplementationCodeFilePathsProviderAction = Instances.ServiceAction.AddServiceImplementationCodeFilePathsProviderAction();
            var serviceImplementationTypeIdentifierAction = Instances.ServiceAction.AddServiceImplementationTypeIdentifierAction();

            // Level 01.
            var projectFilePathsProviderAction = Instances.ServiceAction.AddProjectFilePathsProviderAction(
                projectRepositoryAction);

            // Operations.

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
                .Run(o999_ScratchAction)
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