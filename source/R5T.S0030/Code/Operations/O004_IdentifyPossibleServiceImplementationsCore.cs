using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O004_IdentifyPossibleServiceImplementationsCore : IOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }


        public O004_IdentifyPossibleServiceImplementationsCore(
            ILogger<O004_IdentifyPossibleServiceImplementationsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
        }

        public async Task<List<ReasonedServiceComponentDescriptor>> Run()
        {
            var possibleServiceImplementationDescriptors = new List<ReasonedServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying possible service implementations...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogDebug($"Evaluating project:\n{projectFilePath}");

                var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

                // /Services/Implementations directory code file paths, but without proper service implementation marker attribute (the marker interface is suggested, later).
                {
                    var servicesImplementationsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(projectDirectoryPath);

                    var directoryExists = Instances.FileSystemOperator.DirectoryExists(servicesImplementationsDirectoryPath);
                    if (directoryExists)
                    {
                        var servicesImplementationsCodeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(servicesImplementationsDirectoryPath);

                        foreach (var servicesImplementationsCodeFilePath in servicesImplementationsCodeFilePaths)
                        {
                            var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                                servicesImplementationsCodeFilePath,
                                compilationUnit =>
                                {
                                    var classes = compilationUnit.GetClasses();

                                    var classesOfInterest = classes
                                        .Where(Instances.ClassOperator.LacksServiceDefinitionMarkerAttribute)
                                        .Now();

                                    var classTypeNames = classesOfInterest.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                                    return Task.FromResult(classTypeNames);
                                });

                            var currentServiceImplementationsDescriptors = typeNamedCodeFilePaths
                                .Select(x => x.GetReasonedServiceComponentDescriptor(
                                    projectFilePath,
                                    Reasons.LacksMarkerAttribute))
                                .Now();

                            possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationsDescriptors);
                        }
                    }
                }

                this.Logger.LogInformation("Checked /Services/Classes directory for classes without proper service implementation marker attribute.");

                // /Services/Classes directory code file paths, any class.
                {
                    var servicesClassesDirectoryPath = Instances.ProjectPathsOperator.GetServicesClassesDirectoryPath(projectDirectoryPath);

                    var currentServiceImplementationDescriptors = await Instances.Operation.CheckDirectoryForClasses(
                        servicesClassesDirectoryPath,
                        projectFilePath);

                    possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors
                        .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InOldServicesClassesDirectory)));

                    this.Logger.LogInformation("Checked /Services/Classes directory for any classes.");
                }

                // /Services/Definitions directory code file paths, any class.
                {
                    var servicesDefinitionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesDefinitionsDirectoryPath(projectDirectoryPath);

                    var currentServiceImplementationDescriptors = await Instances.Operation.CheckDirectoryForClasses(
                        servicesDefinitionsDirectoryPath,
                        projectFilePath);

                    possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors
                        .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.ClassInServicesDefinitionsDirectory)));

                    this.Logger.LogInformation("Checked /Services/Definitions directory for any classes.");
                }

                // /Services/Interfaces directory code file paths, any class.
                {
                    var servicesInterfacesDirectoryPath = Instances.ProjectPathsOperator.GetServicesInterfacesDirectoryPath(projectDirectoryPath);

                    var currentServiceImplementationDescriptors = await Instances.Operation.CheckDirectoryForClasses(
                        servicesInterfacesDirectoryPath,
                        projectFilePath);

                    possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors
                        .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.ClassInServicesInterfacesDirectory)));

                    this.Logger.LogInformation("Checked /Services/Interfaces directory for any classes.");
                }

                this.Logger.LogInformation($"Evaluated project:\n{projectFilePath}");
            }

            this.Logger.LogInformation("Identified service implementations.");

            return possibleServiceImplementationDescriptors;
        }
    }
}
