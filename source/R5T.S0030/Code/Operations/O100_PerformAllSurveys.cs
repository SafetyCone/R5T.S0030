using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    public class O100_PerformAllSurveys : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private IServiceDefinitionCodeFilePathsProvider ServiceDefinitionCodeFilePathsProvider { get; }
        private IServiceDefinitionTypeIdentifier ServiceDefinitionTypeIdentifier { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private IServiceImplementationTypeIdentifier ServiceImplementationTypeIdentifier { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceComponents { get; }
        private O002A_DescribePossibleServiceComponents O002A_DescribePossibleServiceComponents { get; }


        public O100_PerformAllSurveys(
            ILogger<O100_PerformAllSurveys> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceDefinitionCodeFilePathsProvider serviceDefinitionCodeFilePathsProvider,
            IServiceDefinitionTypeIdentifier serviceDefinitionTypeIdentifier,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            IServiceImplementationTypeIdentifier serviceImplementationTypeIdentifier,
            O001A_DescribeServiceComponents o001A_DescribeServiceComponents,
            O002A_DescribePossibleServiceComponents o002A_DescribePossibleServiceComponents)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceDefinitionCodeFilePathsProvider = serviceDefinitionCodeFilePathsProvider;
            this.ServiceDefinitionTypeIdentifier = serviceDefinitionTypeIdentifier;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceImplementationTypeIdentifier = serviceImplementationTypeIdentifier;
            this.O001A_DescribeServiceComponents = o001A_DescribeServiceComponents;
            this.O002A_DescribePossibleServiceComponents = o002A_DescribePossibleServiceComponents;
        }

        public async Task Run()
        {
            /// Inputs.
            var definitionsFoundTextFilePath = @"C:\Temp\Service Definitions-Found.txt";
            var possibleDefinitionsTextFilePath = @"C:\Temp\Service Definitions-Possible.txt";
            var possibleDefinitionsByReasonTextFilePath = @"C:\Temp\Service Definitions-Possible, by Reason.txt";
            var implementationsFoundTextFilePath = @"C:\Temp\Service Implementations-Found.txt";
            var possibleImplementationsTextFilePath = @"C:\Temp\Service Implementations-Possible.txt";
            var possibleImplementationsByReasonTextFilePath = @"C:\Temp\Service Implementations-Possible, by Reason.txt";
            var definitionsMissingMarkerInterfaceTextFilePath = @"C:\Temp\Service Definitions-Missing marker interface.txt";
            var implementationsMissingMarkerInterfacesTextFilePath = @"C:\Temp\Service Implementations-Missing marker interface.txt";

            /// Run.
            var definitionDescriptors = new List<IServiceDefinitionDescriptor>();
            var possibleDefinitionDescriptors = new List<IReasonedServiceComponentDescriptor>();
            var implementationDescriptors = new List<IServiceComponentDescriptor>();
            var possibleImplementationDescriptors = new List<IReasonedServiceComponentDescriptor>();
            var descriptorsOfImproperServiceDefinitions = new List<IReasonedServiceComponentDescriptor>();
            var descriptorsOfImproperServiceImplementations = new List<IReasonedServiceComponentDescriptor>();

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                await Instances.Operation.IdentifyServiceDefinitionsInProject(
                    projectFilePath,
                    this.ServiceDefinitionCodeFilePathsProvider,
                    this.ServiceDefinitionTypeIdentifier,
                    definitionDescriptors);

                await Instances.Operation.IdentifyPossibleServiceDefinitionsInProject(
                    projectFilePath,
                    this.Logger,
                    possibleDefinitionDescriptors);

                await Instances.Operation.IdentifyServiceImplementationsInProject(
                    projectFilePath,
                    this.ServiceImplementationCodeFilePathsProvider,
                    this.ServiceImplementationTypeIdentifier,
                    implementationDescriptors);

                await Instances.Operation.IdentifyPossibleServiceImplementationsInProject(
                    projectFilePath,
                    this.Logger,
                    possibleImplementationDescriptors);

                await Instances.Operation.IdentifyServiceDefinitionsLackingMarkerInterfaceInProject(
                    projectFilePath,
                    descriptorsOfImproperServiceDefinitions);

                await Instances.Operation.IdentifyServiceImplementationsLackingMarkerInterfaceInProject(
                    projectFilePath,
                    descriptorsOfImproperServiceImplementations);

                this.Logger.LogInformation($"Evaluated project:\n{projectFilePath}");
            }

            var possibleDefinitionReasonsSortOrder = new SpecifiedListComparer<string>(
                Reasons.LacksMarkerAttribute,
                Reasons.InOldServicesInterfacesDirectory);

            var possibleImplementationReasonsSortOrder = new SpecifiedListComparer<string>(
                Reasons.LacksMarkerAttribute,
                Reasons.InOldServicesClassesDirectory);

            await this.O001A_DescribeServiceComponents.Run(
                definitionsFoundTextFilePath,
                definitionDescriptors);

            await this.O001A_DescribeServiceComponents.Run(
                possibleDefinitionsTextFilePath,
                possibleDefinitionDescriptors);

            await this.O002A_DescribePossibleServiceComponents.Run(
                possibleDefinitionsByReasonTextFilePath,
                possibleDefinitionDescriptors,
                possibleDefinitionReasonsSortOrder);

            await this.O001A_DescribeServiceComponents.Run(
                implementationsFoundTextFilePath,
                implementationDescriptors);

            await this.O001A_DescribeServiceComponents.Run(
                possibleImplementationsTextFilePath,
                possibleImplementationDescriptors);

            await this.O002A_DescribePossibleServiceComponents.Run(
               possibleImplementationsByReasonTextFilePath,
               possibleImplementationDescriptors,
               possibleImplementationReasonsSortOrder);

            await this.O001A_DescribeServiceComponents.Run(
                definitionsMissingMarkerInterfaceTextFilePath,
                descriptorsOfImproperServiceDefinitions);

            await this.O001A_DescribeServiceComponents.Run(
                implementationsMissingMarkerInterfacesTextFilePath,
                descriptorsOfImproperServiceImplementations);

            // Open in Notepad++.
            await this.NotepadPlusPlusOperator.OpenFilePath(definitionsFoundTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(possibleDefinitionsTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(possibleDefinitionsByReasonTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(implementationsFoundTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(possibleImplementationsTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(possibleImplementationsByReasonTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(definitionsMissingMarkerInterfaceTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(implementationsMissingMarkerInterfacesTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O100_PerformAllSurveys)}");
        }
    }
}
