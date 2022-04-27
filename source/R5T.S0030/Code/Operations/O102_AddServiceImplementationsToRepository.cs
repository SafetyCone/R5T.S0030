using System;
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
    /// <summary>
    /// Initial attempt.
    /// See <see cref="O105B_AddServiceImplementationsToRepository"/>.
    /// </summary>
    [OperationMarker]
    public class O102_AddServiceImplementationsToRepository : IActionOperation
    {
        private IHumanOutput HumanOutput { get; }
        private IHumanOutputFilePathProvider HumanOutputFilePathProvider { get; }
        private ILogger Logger { get; }
        private IMainFileContextFilePathsProvider MainFileContextFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectRepository ProjectRepository { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }
        private O003_IdentifyServiceImplementationsCore O003_IdentifyServiceImplementationsCore { get; }


        public O102_AddServiceImplementationsToRepository(
            IHumanOutput humanOutput,
            IHumanOutputFilePathProvider humanOutputFilePathProvider,
            ILogger<O101_AddServiceDefinitionsToRepository> logger,
            IMainFileContextFilePathsProvider mainFileContextFilePathsProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectRepository projectRepository,
            Repositories.IServiceRepository serviceRepository,
            O003_IdentifyServiceImplementationsCore o003_IdentifyServiceImplementationsCore)
        {
            this.HumanOutput = humanOutput;
            this.HumanOutputFilePathProvider = humanOutputFilePathProvider;
            this.Logger = logger;
            this.MainFileContextFilePathsProvider = mainFileContextFilePathsProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectRepository = projectRepository;
            this.ServiceRepository = serviceRepository;
            this.O003_IdentifyServiceImplementationsCore = o003_IdentifyServiceImplementationsCore;
        }

        public async Task Run()
        {
            this.Logger.LogInformation("Getting all current service implementations...");

            var currentServiceImplementationDescriptors = await this.O003_IdentifyServiceImplementationsCore.Run();

            //// Use a cache-file during debugging for speed. (Avoids needing to search the whole file system for each debugging run.)
            //var currentServiceImplementationDescriptorsCacheFilePath = @"C:\Temp\Service implementation descriptors.json";

            //Newtonsoft.Json.JsonFileHelper.WriteToFile(
            //    currentServiceImplementationDescriptorsCacheFilePath,
            //    currentServiceImplementationDescriptors);

            //var currentServiceImplementationDescriptors = Newtonsoft.Json.JsonFileHelper.LoadFromFile<ServiceComponentDescriptor[]>(
            //    currentServiceImplementationDescriptorsCacheFilePath)
            //    .Cast<IServiceComponentDescriptor>()
            //    .ToList();

            var currentServiceImplementationDescriptorsCount = currentServiceImplementationDescriptors.Count;
            this.HumanOutput.WriteLine($"Found {currentServiceImplementationDescriptorsCount} current service implementations.");

            /// Service implementations.
            this.Logger.LogInformation("Getting all repository service implementations...");

            var repositoryServiceImplementations = await this.ServiceRepository.GetAllServiceImplementations();

            var repositoryServiceImplementationsCount = repositoryServiceImplementations.Length;
            this.HumanOutput.WriteLine($"Repository contains {repositoryServiceImplementationsCount} service implementations.");

            this.Logger.LogInformation("Determining new and departed service implementations...");

            // Verify data is distinct by (type name-code file path) pair.
            currentServiceImplementationDescriptors.VerifyDistinctByNamedFilePathedData();
            repositoryServiceImplementations.VerifyDistinctByNamedFilePathedData();

            // Find new service definitions.
            var newServiceImplementationDescriptors = currentServiceImplementationDescriptors.Except(
                repositoryServiceImplementations,
                NamedFilePathedEqualityComparer.Instance)
                .Cast<IServiceComponentDescriptor>()
                .Now();

            var newServiceImplementationDescriptorsCount = newServiceImplementationDescriptors.Length;
            this.HumanOutput.WriteLine($"Found {newServiceImplementationDescriptorsCount} new service implementations.");

            // Find departed service definitions.
            var departedServiceImplementations = repositoryServiceImplementations.Except(
                currentServiceImplementationDescriptors,
                NamedFilePathedEqualityComparer.Instance)
                .Cast<Repositories.ServiceImplementation>()
                .Now();

            var departedServiceImplementationsCount = departedServiceImplementations.Length;
            this.HumanOutput.WriteLine($"Found {departedServiceImplementationsCount} departed service implementations.");

            this.Logger.LogInformation("Adding new service implementations to the repository...");

            var newServiceImplementationDataSets = newServiceImplementationDescriptors.GetRepositoryServiceComponentDataSets();

            //await this.ServiceRepository.AddServiceDefinitions(newServiceDefinitionDataSets);
            var newRepositoryServiceDefinitionsByIdentity = await this.ServiceRepository.AddServiceImplementations(newServiceImplementationDataSets);

            this.Logger.LogInformation("Removing departed service implementations from the repository...");

            await this.ServiceRepository.DeleteServiceImplementations(departedServiceImplementations);

            /// To-project mappings.
            this.Logger.LogInformation("Getting all repository service implementation-to-project mappings...");

            var repositoryServiceImplementationToProjectMappings = await this.ServiceRepository.GetAllServiceImplementationToProjectMappings();

            var repositoryServiceImplementationToProjectMappingsCount = repositoryServiceImplementationToProjectMappings.Length;
            this.HumanOutput.WriteLine($"Repository contains {repositoryServiceImplementationToProjectMappingsCount} service implementation-to-project mappings.");

            var repositoryProjectsByServiceImplementation = repositoryServiceImplementationToProjectMappings
                .ToDictionary(
                    x => x.serviceImplementation,
                    x => x.project,
                    NamedFilePathedEqualityComparer<Repositories.ServiceImplementation>.Instance);

            // Compute data.
            this.Logger.LogInformation("Computing current service implementation-to-project mappings...");

            var newRepositoryServiceImplementations = repositoryServiceImplementations
                .Except(departedServiceImplementations, NamedFilePathedEqualityComparer<Repositories.ServiceImplementation>.Instance)
                .Concat(newRepositoryServiceDefinitionsByIdentity.Values)
                .Now();

            var currentServiceImplementationDescriptorsByRepositoryServiceImplementation = currentServiceImplementationDescriptors
                .Join(newRepositoryServiceImplementations,
                    x => x,
                    y => y,
                    (x, y) => (ServiceDefinitionDescriptor: x, ServiceDefinition: y),
                    NamedFilePathedEqualityComparer.Instance)
                .ToDictionary(
                    x => x.ServiceDefinition,
                    x => x.ServiceDefinitionDescriptor,
                    NamedFilePathedEqualityComparer<Repositories.ServiceImplementation>.Instance);

            var currentProjectFilePathsByRepositoryServiceImplementation = currentServiceImplementationDescriptorsByRepositoryServiceImplementation
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.ProjectFilePath,
                    NamedFilePathedEqualityComparer<Repositories.ServiceImplementation>.Instance);

            var projects = await this.ProjectRepository.GetAllProjects();

            var projectsByProjectFilePath = projects.ToDictionaryByFilePath();

            var currentProjectsByServiceImplementation = currentProjectFilePathsByRepositoryServiceImplementation
                .Join(projectsByProjectFilePath,
                    x => x.Value,
                    y => y.Key,
                    (x, y) => (ServiceDefinition: x.Key, Project: y.Value))
                .ToDictionary(
                    x => x.ServiceDefinition,
                    x => x.Project,
                    NamedFilePathedEqualityComparer<Repositories.ServiceImplementation>.Instance);

            this.Logger.LogInformation("Determining new and departed service implementation-to-project mappings...");

            // Find new service definition-to-project mappings.
            var newProjectsByServiceImplementation = currentProjectsByServiceImplementation.Except(
                repositoryProjectsByServiceImplementation,
                ProjectByServiceImplementationIdentitiesEqualityComparer.Instance)
                .Now();

            var newProjectsByServiceImplementationCount = newProjectsByServiceImplementation.Length;
            this.HumanOutput.WriteLine($"Found {newProjectsByServiceImplementationCount} new service implementation-to-project mappings.");

            // Find departed service definitions.
            var departedProjectsByServiceImplementation = repositoryProjectsByServiceImplementation.Except(
                currentProjectsByServiceImplementation,
                ProjectByServiceImplementationIdentitiesEqualityComparer.Instance)
                .Now();

            var departedProjectsByServiceDefinitionCount = departedProjectsByServiceImplementation.Length;
            this.HumanOutput.WriteLine($"Found {departedProjectsByServiceDefinitionCount} departed service implementation-to-project mappings.");

            this.Logger.LogInformation("Adding new service implementation-to-project mappings to the repository...");

            await this.ServiceRepository.AddProjectsForServiceImplementations(newProjectsByServiceImplementation
                .Select(x => (x.Key, x.Value)));

            this.Logger.LogInformation("Removing departed service implementation-to-project mappings from the repository...");

            await this.ServiceRepository.DeleteProjectsForServiceImplementations(departedProjectsByServiceImplementation
                .Select(x => (x.Key, x.Value)));

            /// Display human output.
            var humanOutputFilePath = await this.HumanOutputFilePathProvider.GetHumanOutputFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(humanOutputFilePath);

            // Display the service definitions and to-project mappings files.
            var serviceImplementationsFilePath = await this.MainFileContextFilePathsProvider.GetServiceImplementationsJsonFilePath();
            var toProjectMappingsFilePath = await this.MainFileContextFilePathsProvider.GetServiceComponentToProjectMappingsJsonFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(serviceImplementationsFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(toProjectMappingsFilePath);
        }
    }
}
