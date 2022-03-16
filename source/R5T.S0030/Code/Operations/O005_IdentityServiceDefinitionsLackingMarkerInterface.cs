using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    /// <summary>
    /// Identifies service definitions that have the required service definition marker attribute, but lack the suggested service definition marker interface.
    /// </summary>
    [OperationMarker]
    public class O005_IdentityServiceDefinitionsLackingMarkerInterface : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceComponents { get; }


        public O005_IdentityServiceDefinitionsLackingMarkerInterface(
            ILogger<O005_IdentityServiceDefinitionsLackingMarkerInterface> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectFilePathsProvider projectFilePathsProvider,
            O001A_DescribeServiceComponents o001A_DescribeServiceDefinitions)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.O001A_DescribeServiceComponents = o001A_DescribeServiceDefinitions;
        }

        public async Task Run()
        {
            /// Inputs.
            var outputTextFilePath = @"C:\Temp\Service Definitions-Missing marker interface.txt";

            /// Run.
            var descriptorsOfImproperServiceDefinitions = new List<IServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying service definitions with marker attribute, but without marker interface...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogDebug($"Evaluating project:\n{projectFilePath}");

                var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

                var servicesDefinitionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesDefinitionsDirectoryPath(projectDirectoryPath);

                var directoryExists = Instances.FileSystemOperator.DirectoryExists(servicesDefinitionsDirectoryPath);
                if (directoryExists)
                {
                    var servicesDefinitionsCodeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(servicesDefinitionsDirectoryPath);

                    foreach (var servicesDefinitionsCodeFilePath in servicesDefinitionsCodeFilePaths)
                    {
                        var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                            servicesDefinitionsCodeFilePath,
                            compilationUnit =>
                            {
                                var interfaces = compilationUnit.GetInterfaces();

                                var interfacesOfInterest = interfaces
                                    .Where(x => true
                                        && Instances.InterfaceOperator.HasServiceDefinitionMarkerAttribute(x)
                                        && Instances.InterfaceOperator.LacksServiceDefinitionMarkerInterface(x))
                                    .Now();

                                var interfaceTypeNames = interfacesOfInterest.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                                return Task.FromResult(interfaceTypeNames);
                            });

                        var serviceComponentDescriptors = typeNamedCodeFilePaths
                            .Select(x => x.GetReasonedServiceComponentDescriptor(
                                projectFilePath,
                                Reasons.LacksMarkerInterface))
                            .Now();

                        descriptorsOfImproperServiceDefinitions.AddRange(serviceComponentDescriptors);
                    }
                }
            }

            this.Logger.LogInformation("Identified service definitions with marker attribute, but without marker interface.");

            await this.O001A_DescribeServiceComponents.Run(
                outputTextFilePath,
                descriptorsOfImproperServiceDefinitions);

            await this.NotepadPlusPlusOperator.OpenFilePath(outputTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O002_IdentifyPossibleServiceDefinitions)}");
        }
    }
}
