using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0098;


namespace R5T.S0030
{
    public static partial class IOperationExtensions
    {
        public static async Task<ServiceComponentDescriptor[]> CheckDirectoryForInterfaces(this IOperation _,
            string directoryPath,
            string projectFilePath)
        {
            // Be defensive.
            var directoryExists = Instances.FileSystemOperator.DirectoryExists(directoryPath);
            if(!directoryExists)
            {
                return Array.Empty<ServiceComponentDescriptor>();
            }

            var possibleServiceComponentDescriptors = new List<ServiceComponentDescriptor>();

            var codeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(directoryPath);

            foreach (var codeFilePath in codeFilePaths)
            {
                var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                    codeFilePath,
                    compilationUnit =>
                    {
                        var interfaces = compilationUnit.GetInterfaces();

                        var interfaceTypeNames = interfaces.GetNamespacedTypeNames_HandlingTypeParameters().Now();

                        return Task.FromResult(interfaceTypeNames);
                    });

                var currentServiceComponentDescriptors = typeNamedCodeFilePaths
                    .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                    .Now();

                possibleServiceComponentDescriptors.AddRange(currentServiceComponentDescriptors);
            }

            return possibleServiceComponentDescriptors.ToArray();
        }

        public static async Task<ServiceComponentDescriptor[]> CheckDirectoryForClasses(this IOperation _,
            string directoryPath,
            string projectFilePath)
        {
            // Be defensive.
            var directoryExists = Instances.FileSystemOperator.DirectoryExists(directoryPath);
            if (!directoryExists)
            {
                return Array.Empty<ServiceComponentDescriptor>();
            }

            var possibleServiceComponentDescriptors = new List<ServiceComponentDescriptor>();

            var codeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(directoryPath);

            foreach (var codeFilePath in codeFilePaths)
            {
                var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                    codeFilePath,
                    compilationUnit =>
                    {
                        var classes = compilationUnit.GetClasses();

                        var classTypeNames = classes.GetNamespacedTypeNames_HandlingTypeParameters().Now();

                        return Task.FromResult(classTypeNames);
                    });

                var currentServiceComponentDescriptors = typeNamedCodeFilePaths
                    .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                    .Now();

                possibleServiceComponentDescriptors.AddRange(currentServiceComponentDescriptors);
            }

            return possibleServiceComponentDescriptors.ToArray();
        }

        public static async Task<ITypeNamedCodeFilePathed[]> GetTypeNamedCodeFilePatheds(this IOperation _,
            string codeFilepath,
            Func<CompilationUnitSyntax, Task<string[]>> getTypeNamesFromCompilationUnitFunction)
        {
            // Be defensive.
            var fileExists = Instances.FileSystemOperator.FileExists(codeFilepath);
            if (!fileExists)
            {
                return Array.Empty<ITypeNamedCodeFilePathed>();
            }

            var compilationUnit = await Instances.CompilationUnitOperator_Old.Load(
                codeFilepath);

            var typeNames = await getTypeNamesFromCompilationUnitFunction(compilationUnit);

            var output = typeNames
                .Select(typeName => new TypeNamedCodeFilePathed
                {
                    CodeFilePath = codeFilepath,
                    TypeName = typeName,
                })
                .Now();

            return output;
        }

        public static async Task IdentifyServiceDefinitionsInProject(this IOperation _,
            string projectFilePath,
            IServiceDefinitionCodeFilePathsProvider serviceDefinitionCodeFilePathsProvider,
            IServiceDefinitionTypeIdentifier serviceDefinitionTypeIdentifier,
            List<IServiceDefinitionDescriptor> serviceDefinitionDescriptors)
        {
            var serviceDefinitionCodeFilePaths = await serviceDefinitionCodeFilePathsProvider.GetServiceDefinitionCodeFilePaths(
                    projectFilePath);

            foreach (var serviceDefinitionCodeFilePath in serviceDefinitionCodeFilePaths)
            {
                var typeNamedCodeFilePaths = await serviceDefinitionTypeIdentifier.GetServiceDefinitionTypes(
                    serviceDefinitionCodeFilePath);

                var currentServiceDefinitionDescriptors = typeNamedCodeFilePaths
                    .Select(x => x.GetServiceDefinitionDescriptor(projectFilePath))
                    .Now();

                serviceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors);
            }
        }

        public static async Task IdentifyPossibleServiceDefinitionsInProject(this IOperation _,
            string projectFilePath,
            ILogger logger,
            List<IReasonedServiceComponentDescriptor> possibleServiceDefinitionDescriptors)
        {
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

            // /Services/Definitions directory code file paths, but without service definition marker attribute. Note: service definition marker interface will be a suggestion, later.
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
                                    .Where(Instances.InterfaceOperator_Old.LacksServiceDefinitionMarkerAttribute)
                                    .Now();

                                var interfaceTypeNames = interfacesOfInterest.GetNamespacedTypeNames_HandlingTypeParameters().Now();

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

            logger.LogInformation("Checked /Services/Definitions directory for interfaces without proper service definition marker attribute.");

            // /Services/Definitions directory, and has ServiceDefinitionMarkerAttribute, but it is not the R5T.T0064.ServiceDefinitionMarkerAttribute (it is perhaps the old R5T.Dacia attribute).
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
                                    .Where(Instances.InterfaceOperator_Old.HasServiceDefinitionMarkerAttribute)
                                    .Where(x => Instances.InterfaceOperator_Old.LacksR5T_T0064_ServiceDefinitionMarkerAttribute(x, compilationUnit))
                                    .Now();

                                var interfaceTypeNames = interfacesOfInterest.GetNamespacedTypeNames_HandlingTypeParameters().Now();

                                return Task.FromResult(interfaceTypeNames);
                            });

                        var currentServiceDefinitionDescriptors = typeNamedCodeFilePaths
                            .Select(x => x.GetReasonedServiceComponentDescriptor(
                                projectFilePath,
                                Reasons.WrongMarkerAttribute))
                            .Now();

                        possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors);
                    }
                }
            }

            logger.LogInformation("Checked /Services/Definitions directory for interfaces with a service definition marker attribute, but not the R5T.T0064.ServiceDefinitionMarkerAttribute.");

            // /Services/Interfaces directory code file paths, any interface.
            {
                var servicesInterfacesDirectoryPath = Instances.ProjectPathsOperator.GetServicesInterfacesDirectoryPath(projectDirectoryPath);

                var currentServiceDefinitionDescriptors = await Instances.Operation.CheckDirectoryForInterfaces(
                    servicesInterfacesDirectoryPath,
                    projectFilePath);

                possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors
                    .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InOldServicesInterfacesDirectory)));

                logger.LogInformation("Checked /Services/Interfaces directory for any interfaces.");
            }

            // /Services/Implementations directory code file paths, any interface.
            {
                var servicesImplementationsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(projectDirectoryPath);

                var currentServiceDefinitionDescriptors = await Instances.Operation.CheckDirectoryForInterfaces(
                    servicesImplementationsDirectoryPath,
                    projectFilePath);

                possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors
                    .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InterfaceInServicesImplementationsDirectory)));

                logger.LogInformation("Checked /Services/Implementations directory for any interfaces.");
            }

            // /Services/Classes directory code file paths, any interface.
            {
                var servicesClassesDirectoryPath = Instances.ProjectPathsOperator.GetServicesClassesDirectoryPath(projectDirectoryPath);

                var currentServiceDefinitionDescriptors = await Instances.Operation.CheckDirectoryForInterfaces(
                    servicesClassesDirectoryPath,
                    projectFilePath);

                possibleServiceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors
                    .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InterfaceInServicesClassesDirectory)));

                logger.LogInformation("Checked /Services/Classes directory for any interfaces.");
            }
        }

        public static async Task IdentifyPossibleServiceImplementationsInProject(this IOperation _,
            string projectFilePath,
            ILogger logger,
            List<IReasonedServiceComponentDescriptor> possibleServiceImplementationDescriptors)
        {
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

            // /Services/Implementations directory code file paths, but without service implementation marker attribute (the marker interface is suggested, later).
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
                                    // Service implementations are not abstract and not static.
                                    .Where(x => !x.IsAbstract() && !x.IsStatic())
                                    .Where(Instances.ClassOperator_Old.LacksServiceDefinitionMarkerAttribute)
                                    .Now();

                                var classTypeNames = classesOfInterest.GetNamespacedTypeNames_HandlingTypeParameters().Now();

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

            logger.LogInformation("Checked /Services/Classes directory for classes without proper service implementation marker attribute.");

            // /Services/Implementations directory, and has ServiceImplementationMarkerAttribute, but it is not the R5T.T0064.ServiceImplementationMarkerAttribute (it is perhaps the old R5T.Dacia attribute).
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
                                    // Service implementations are not abstract.
                                    .Where(x => !x.IsAbstract() && !x.IsStatic())
                                    .Where(Instances.ClassOperator_Old.HasServiceImplementationMarkerAttribute)
                                    .Where(x => Instances.ClassOperator_Old.LacksR5T_T0064_ServiceImplementationMarkerAttribute(x, compilationUnit))
                                    .Now();

                                var classTypeNames = classesOfInterest.GetNamespacedTypeNames_HandlingTypeParameters().Now();

                                return Task.FromResult(classTypeNames);
                            });

                        var currentServiceImplementationsDescriptors = typeNamedCodeFilePaths
                            .Select(x => x.GetReasonedServiceComponentDescriptor(
                                projectFilePath,
                                Reasons.WrongMarkerAttribute))
                            .Now();

                        possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationsDescriptors);
                    }
                }
            }

            logger.LogInformation("Checked /Services/Definitions directory for interfaces with a service definition marker attribute, but not the R5T.T0064.ServiceDefinitionMarkerAttribute.");

            // /Services/Classes directory code file paths, any class.
            {
                var servicesClassesDirectoryPath = Instances.ProjectPathsOperator.GetServicesClassesDirectoryPath(projectDirectoryPath);

                var currentServiceImplementationDescriptors = await Instances.Operation.CheckDirectoryForClasses(
                    servicesClassesDirectoryPath,
                    projectFilePath);

                possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors
                    .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.InOldServicesClassesDirectory)));

                logger.LogInformation("Checked /Services/Classes directory for any classes.");
            }

            // /Services/Definitions directory code file paths, any class.
            {
                var servicesDefinitionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesDefinitionsDirectoryPath(projectDirectoryPath);

                var currentServiceImplementationDescriptors = await Instances.Operation.CheckDirectoryForClasses(
                    servicesDefinitionsDirectoryPath,
                    projectFilePath);

                possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors
                    .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.ClassInServicesDefinitionsDirectory)));

                logger.LogInformation("Checked /Services/Definitions directory for any classes.");
            }

            // /Services/Interfaces directory code file paths, any class.
            {
                var servicesInterfacesDirectoryPath = Instances.ProjectPathsOperator.GetServicesInterfacesDirectoryPath(projectDirectoryPath);

                var currentServiceImplementationDescriptors = await Instances.Operation.CheckDirectoryForClasses(
                    servicesInterfacesDirectoryPath,
                    projectFilePath);

                possibleServiceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors
                    .Select(x => x.GetReasonedServiceComponentDescriptor(Reasons.ClassInServicesInterfacesDirectory)));

                logger.LogInformation("Checked /Services/Interfaces directory for any classes.");
            }
        }

        public static async Task IdentifyServiceImplementationsInProject(this IOperation _,
            string projectFilePath,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            IServiceImplementationTypeIdentifier serviceImplementationTypeIdentifier,
            List<IServiceComponentDescriptor> serviceImplementationDescriptors)
        {
            var serviceImplementationCodeFilePaths = await serviceImplementationCodeFilePathsProvider.GetServiceImplementationCodeFilePaths(
                    projectFilePath);

            foreach (var serviceDefinitionCodeFilePath in serviceImplementationCodeFilePaths)
            {
                var typeNamedCodeFilePaths = await serviceImplementationTypeIdentifier.GetServiceImplementationTypes(
                    serviceDefinitionCodeFilePath);

                var currentServiceImplementationDescriptors = typeNamedCodeFilePaths
                    .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                    .Now();

                serviceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors);
            }
        }
        
        public static async Task IdentifyServiceDefinitionsLackingMarkerInterfaceInProject(this IOperation _,
            string projectFilePath,
            List<IReasonedServiceComponentDescriptor> descriptorsOfImproperServiceDefinitions)
        {
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
                                    && Instances.InterfaceOperator_Old.HasServiceDefinitionMarkerAttribute(x)
                                    && Instances.InterfaceOperator_Old.LacksServiceDefinitionMarkerInterface(x))
                                .Now();

                            var interfaceTypeNames = interfacesOfInterest.GetNamespacedTypeNames_HandlingTypeParameters().Now();

                            return Task.FromResult(interfaceTypeNames);
                        });

                    var reasonedServiceComponentDescriptors = typeNamedCodeFilePaths
                        .Select(x => x.GetReasonedServiceComponentDescriptor(
                            projectFilePath,
                            Reasons.LacksMarkerInterface))
                        .Now();

                    descriptorsOfImproperServiceDefinitions.AddRange(reasonedServiceComponentDescriptors);
                }
            }
        }

        public static async Task IdentifyServiceImplementationsLackingMarkerInterfaceInProject(this IOperation _,
            string projectFilePath,
            List<IReasonedServiceComponentDescriptor> descriptorsOfImproperServiceImplementations)
        {
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

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
                                .Where(x => true
                                    && Instances.ClassOperator_Old.HasServiceImplementationMarkerAttribute(x)
                                    && Instances.ClassOperator_Old.LacksServiceDefinitionMarkerInterface(x))
                                .Now();

                            var classTypeNames = classesOfInterest.GetNamespacedTypeNames_HandlingTypeParameters().Now();

                            return Task.FromResult(classTypeNames);
                        });

                    var serviceComponentDescriptors = typeNamedCodeFilePaths
                        .Select(x => x.GetReasonedServiceComponentDescriptor(
                            projectFilePath,
                            Reasons.LacksMarkerInterface))
                        .Now();

                    descriptorsOfImproperServiceImplementations.AddRange(serviceComponentDescriptors);
                }
            }
        }
    }
}
