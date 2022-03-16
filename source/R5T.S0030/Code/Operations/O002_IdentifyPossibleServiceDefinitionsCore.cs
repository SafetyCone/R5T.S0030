using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;


namespace R5T.S0030
{
    /// <summary>
    /// Survey all projects in the project repository, find service definitions, then write them out to a file.
    /// </summary>
    [OperationMarker]
    public class O002_IdentifyPossibleServiceDefinitionsCore : IOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }


        public O002_IdentifyPossibleServiceDefinitionsCore(
            ILogger<O002_IdentifyPossibleServiceDefinitionsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
        }

        public async Task<List<ReasonedServiceComponentDescriptor>> Run()
        {
            var possibleServiceDefinitionDescriptors = new List<ReasonedServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying possible service definitions...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogDebug($"Evaluating project:\n{projectFilePath}");

                var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

                // /Services/Definitions directory code file paths, but without proper service definition marker attribute. Note: service definition marker interface will be a suggestion, later.
                {
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
                                        .Where(Instances.InterfaceOperator.LacksServiceDefinitionMarkerAttribute)
                                        .Now();

                                    var interfaceTypeNames = interfacesOfInterest.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                                    return Task.FromResult(interfaceTypeNames);
                                });

                            var currentServiceDefinitionDescriptors = typeNamedCodeFilePaths
                                .Select(x => x.GetReasonedServiceComponentDescriptor(
                                    projectFilePath,
                                    Reasons.LacksMarkerAttribute))
                                .Now();

                            possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors);
                        }
                    }
                }

                this.Logger.LogInformation("Checked /Services/Definitions directory for interfaces without proper service definition marker attribute.");

                // /Services/Interfaces directory code file paths, any interface.
                {
                    var servicesInterfacesDirectoryPath = Instances.ProjectPathsOperator.GetServicesInterfacesDirectoryPath(projectDirectoryPath);

                    var currentServiceDefinitionDescriptors = await Instances.Operation.CheckDirectoryForInterfaces(
                        servicesInterfacesDirectoryPath,
                        projectFilePath);

                    possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors
                        .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InOldServicesInterfacesDirectory)));

                    this.Logger.LogInformation("Checked /Services/Interfaces directory for any interfaces.");
                }

                // /Services/Implementations directory code file paths, any interface.
                {
                    var servicesImplementationsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(projectDirectoryPath);

                    var currentServiceDefinitionDescriptors = await Instances.Operation.CheckDirectoryForInterfaces(
                        servicesImplementationsDirectoryPath,
                        projectFilePath);

                    possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors
                        .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InterfaceInServicesImplementationsDirectory)));

                    this.Logger.LogInformation("Checked /Services/Implementations directory for any interfaces.");
                }

                // /Services/Classes directory code file paths, any interface.
                {
                    var servicesClassesDirectoryPath = Instances.ProjectPathsOperator.GetServicesClassesDirectoryPath(projectDirectoryPath);

                    var currentServiceDefinitionDescriptors = await Instances.Operation.CheckDirectoryForInterfaces(
                        servicesClassesDirectoryPath,
                        projectFilePath);

                    possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors
                        .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InterfaceInServicesClassesDirectory)));

                    this.Logger.LogInformation("Checked /Services/Classes directory for any interfaces.");
                }

                this.Logger.LogInformation($"Evaluated project:\n{projectFilePath}");
            }

            this.Logger.LogInformation("Identified service definitions.");

            return possibleServiceDefinitionDescriptors;
        }
    }
}
