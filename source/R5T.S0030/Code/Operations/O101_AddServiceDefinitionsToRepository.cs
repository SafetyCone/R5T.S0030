using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0096;
using R5T.D0096.D003;
using R5T.D0101;
using R5T.D0105;
using R5T.T0020;
using R5T.T0094;


namespace R5T.S0030
{
    [OperationMarker]
    public class O101_AddServiceDefinitionsToRepository : IActionOperation
    {
        private IExecutableDirectoryFilePathProvider ExecutableDirectoryFilePathProvider { get; }
        private IHumanOutput HumanOutput { get; }
        private IHumanOutputFilePathProvider HumanOutputFilePathProvider { get; }
        private ILogger Logger { get; }
        private IMainFileContextFilePathsProvider MainFileContextFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectRepository ProjectRepository { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }
        private O001_IdentifyServiceDefinitionsCore O001_IdentifyServiceDefinitionsCore { get; }



        public O101_AddServiceDefinitionsToRepository(
            IExecutableDirectoryFilePathProvider executableDirectoryFilePathProvider,
            IHumanOutput humanOutput,
            IHumanOutputFilePathProvider humanOutputFilePathProvider,
            ILogger<O101_AddServiceDefinitionsToRepository> logger,
            IMainFileContextFilePathsProvider mainFileContextFilePathsProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectRepository projectRepository,
            Repositories.IServiceRepository serviceRepository,
            O001_IdentifyServiceDefinitionsCore o001_IdentifyServiceDefinitionsCore)
        {
            this.ExecutableDirectoryFilePathProvider = executableDirectoryFilePathProvider;
            this.HumanOutput = humanOutput;
            this.HumanOutputFilePathProvider = humanOutputFilePathProvider;
            this.Logger = logger;
            this.MainFileContextFilePathsProvider = mainFileContextFilePathsProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectRepository = projectRepository;
            this.ServiceRepository = serviceRepository;
            this.O001_IdentifyServiceDefinitionsCore = o001_IdentifyServiceDefinitionsCore;
        }

        public async Task Run()
        {
            this.Logger.LogInformation("Getting all current service definitions...");

            // Search the local file system.
            var localServiceDefinitionDescriptors = await this.O001_IdentifyServiceDefinitionsCore.Run();

            // Add external service definitions.
            var externalServiceDefinitionsExecutableDirectoryRelativeFilePath = @"Files\ServiceDefinitions-External.txt";
            var externalServiceDefinitionsFilePath = await this.ExecutableDirectoryFilePathProvider.GetFilePath(
                externalServiceDefinitionsExecutableDirectoryRelativeFilePath);

            var externalServiceDefinitionNamespacedTypeNames = await FileHelper.ReadAllLines_TrimEmpty(externalServiceDefinitionsFilePath);

            var externalServiceDefinitionDescriptors = externalServiceDefinitionNamespacedTypeNames
                .Select(xNamedSpacedTypeName => new ServiceDefinitionDescriptor
                {
                    TypeName = xNamedSpacedTypeName,
                    CodeFilePath = String.Empty,
                    ProjectFilePath = String.Empty,
                });

            // Add external service implementations as service definitions.
            var externalServiceImplementationsExecutableDirectoryRelativeFilePath = @"Files\ServiceImplementations-External.txt";
            var externalServiceImplementationsFilePath = await this.ExecutableDirectoryFilePathProvider.GetFilePath(
                externalServiceImplementationsExecutableDirectoryRelativeFilePath);

            var externalServiceImplementationNamespacedTypeNames = await FileHelper.ReadAllLines_TrimEmpty(externalServiceImplementationsFilePath);

            var externalServiceImplementationDescriptors = externalServiceImplementationNamespacedTypeNames
                .Select(xNamedSpacedTypeName => new ServiceDefinitionDescriptor
                {
                    TypeName = xNamedSpacedTypeName,
                    CodeFilePath = String.Empty,
                    ProjectFilePath = String.Empty,
                });

            // Get the combined list.
            var currentServiceDefinitionDescriptors = localServiceDefinitionDescriptors
                .Concat(externalServiceDefinitionDescriptors)
                .Concat(externalServiceImplementationDescriptors)
                .ToList();

            // Use a cache-file during debugging for speed. (Avoids needing to search the whole file system for each debugging run.)
            //var currentServiceDefinitionDescriptorsCacheFilePath = @"C:\Temp\Service definition descriptors.json";

            //Newtonsoft.Json.JsonFileHelper.WriteToFile(
            //    currentServiceDefinitionDescriptorsCacheFilePath,
            //    currentServiceDefinitionDescriptors);

            //var currentServiceDefinitionDescriptors = Newtonsoft.Json.JsonFileHelper.LoadFromFile<ServiceComponentDescriptor[]>(
            //    currentServiceDefinitionDescriptorsCacheFilePath)
            //    .Cast<IServiceComponentDescriptor>()
            //    .ToList();

            var currentServiceDefinitionDescriptorsCount = currentServiceDefinitionDescriptors.Count;
            this.HumanOutput.WriteLine($"Found {currentServiceDefinitionDescriptorsCount} current service definitions.");

            /// Service definitions.
            this.Logger.LogInformation("Getting all repository service definitions...");

            var repositoryServiceDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var repositoryServiceDefinitionsCount = repositoryServiceDefinitions.Length;
            this.HumanOutput.WriteLine($"Repository contains {repositoryServiceDefinitionsCount} service definitions.");

            this.Logger.LogInformation("Determining new and departed service definitions...");

            // Verify data is distinct by (type name-code file path) pair.
            currentServiceDefinitionDescriptors.VerifyDistinctByNamedFilePathedData();
            repositoryServiceDefinitions.VerifyDistinctByNamedFilePathedData();

            // Find new service definitions.
            var newServiceDefinitionDescriptors = currentServiceDefinitionDescriptors.Except(
                repositoryServiceDefinitions,
                NamedFilePathedEqualityComparer.Instance)
                .Cast<IServiceDefinitionDescriptor>()
                .Now();

            var newServiceDefinitionDescriptorsCount = newServiceDefinitionDescriptors.Length;
            this.HumanOutput.WriteLine($"Found {newServiceDefinitionDescriptorsCount} new service definitions.");

            foreach (var newServiceDefinition in newServiceDefinitionDescriptors)
            {
                this.HumanOutput.WriteLine($"\t{newServiceDefinition.TypeName}");
            }

            // Find departed service definitions.
            var departedServiceDefinitions = repositoryServiceDefinitions.Except(
                currentServiceDefinitionDescriptors,
                NamedFilePathedEqualityComparer.Instance)
                .Cast<Repositories.ServiceDefinition>()
                .Now();

            var departedServiceDefinitionsCount = departedServiceDefinitions.Length;
            this.HumanOutput.WriteLine($"Found {departedServiceDefinitionsCount} departed service definitions.");

            foreach (var departedServiceDefinition in departedServiceDefinitions)
            {
                this.HumanOutput.WriteLine($"\t{departedServiceDefinition.TypeName}");
            }

            this.Logger.LogInformation("Adding new service definitions to the repository...");

            var newServiceDefinitionDataSets = newServiceDefinitionDescriptors.GetRepositoryServiceComponentDataSets();

            //await this.ServiceRepository.AddServiceDefinitions(newServiceDefinitionDataSets);
            var newRepositoryServiceDefinitionsByIdentity = await this.ServiceRepository.AddServiceDefinitions(newServiceDefinitionDataSets);

            this.Logger.LogInformation("Removing departed service definitions from the repository...");

            await this.ServiceRepository.DeleteServiceDefinitions(departedServiceDefinitions);

            /// To-project mappings.
            this.Logger.LogInformation("Getting all repository service definition-to-project mappings...");

            var repositoryServiceDefinitionToProjectMappings = await this.ServiceRepository.GetAllServiceDefinitionToProjectMappings();

            var repositoryServiceDefinitionToProjectMappingsCount = repositoryServiceDefinitionToProjectMappings.Length;
            this.HumanOutput.WriteLine($"Repository contains {repositoryServiceDefinitionToProjectMappingsCount} service definition-to-project mappings.");

            var repositoryProjectsByServiceDefinition = repositoryServiceDefinitionToProjectMappings
                .ToDictionary(
                    x => x.serviceDefinition,
                    x => x.project,
                    NamedFilePathedEqualityComparer<Repositories.ServiceDefinition>.Instance);

            // Compute data.
            this.Logger.LogInformation("Computing current service definition-to-project mappings...");

            var newRepositoryServiceDefinitions = repositoryServiceDefinitions
                .Except(departedServiceDefinitions, NamedFilePathedEqualityComparer<Repositories.ServiceDefinition>.Instance)
                .Concat(newRepositoryServiceDefinitionsByIdentity.Values)
                .Now();

            var currentServiceDefinitionDescriptorsByRepositoryServiceDefinition = currentServiceDefinitionDescriptors
                .Join(newRepositoryServiceDefinitions,
                    x => x,
                    y => y,
                    (x, y) => (ServiceDefinitionDescriptor: x, ServiceDefinition: y),
                    NamedFilePathedEqualityComparer.Instance)
                .ToDictionary(
                    x => x.ServiceDefinition,
                    x => x.ServiceDefinitionDescriptor,
                    NamedFilePathedEqualityComparer<Repositories.ServiceDefinition>.Instance);

            var currentProjectFilePathsByRepositoryServiceDefinition = currentServiceDefinitionDescriptorsByRepositoryServiceDefinition
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.ProjectFilePath,
                    NamedFilePathedEqualityComparer<Repositories.ServiceDefinition>.Instance);

            var projects = await this.ProjectRepository.GetAllProjects();

            var projectsByProjectFilePath = projects.ToDictionaryByFilePath();

            var currentProjectsByServiceDefinition = currentProjectFilePathsByRepositoryServiceDefinition
                .Join(projectsByProjectFilePath,
                    x => x.Value,
                    y => y.Key,
                    (x, y) => (ServiceDefinition: x.Key, Project: y.Value))
                .ToDictionary(
                    x => x.ServiceDefinition,
                    x => x.Project,
                    NamedFilePathedEqualityComparer<Repositories.ServiceDefinition>.Instance);

            this.Logger.LogInformation("Determining new and departed service definition-to-project mappings...");

            // Find new service definition-to-project mappings.
            var newProjectsByServiceDefinition = currentProjectsByServiceDefinition.Except(
                repositoryProjectsByServiceDefinition,
                ProjectByServiceDefinitionIdentitiesEqualityComparer.Instance)
                .Now();

            var newProjectsByServiceDefinitionCount = newProjectsByServiceDefinition.Length;
            this.HumanOutput.WriteLine($"Found {newProjectsByServiceDefinitionCount} new service definition-to-project mappings.");

            // Find departed service definitions.
            var departedProjectsByServiceDefinition = repositoryProjectsByServiceDefinition.Except(
                currentProjectsByServiceDefinition,
                ProjectByServiceDefinitionIdentitiesEqualityComparer.Instance)
                .Now();

            var departedProjectsByServiceDefinitionCount = departedProjectsByServiceDefinition.Length;
            this.HumanOutput.WriteLine($"Found {departedProjectsByServiceDefinitionCount} departed service definition-to-project mappings.");

            this.Logger.LogInformation("Adding new service definition-to-project mappings to the repository...");

            await this.ServiceRepository.AddProjectsForServiceDefinitions(newProjectsByServiceDefinition
                .Select(x => (x.Key, x.Value)));

            this.Logger.LogInformation("Removing departed service definition-to-project mappings from the repository...");

            await this.ServiceRepository.DeleteProjectsForServiceDefinitions(departedProjectsByServiceDefinition
                .Select(x => (x.Key, x.Value)));

            // Display the service definitions and to-project mappings files.
            var serviceDefinitionsFilePath = await this.MainFileContextFilePathsProvider.GetServiceDefinitionsJsonFilePath();
            var toProjectMappingsFilePath = await this.MainFileContextFilePathsProvider.GetServiceComponentToProjectMappingsJsonFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(serviceDefinitionsFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(toProjectMappingsFilePath);

            /// Display human output last.
            var humanOutputFilePath = await this.HumanOutputFilePathProvider.GetHumanOutputFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(humanOutputFilePath);
        }
    }
}
