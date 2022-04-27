using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0096;
using R5T.D0101;
using R5T.T0020;
using R5T.T0092;
using R5T.T0094;


namespace R5T.S0030
{
    public class O105B_AddServiceImplementationsToRepository : IOperation
    {
        private IHumanOutput HumanOutput { get; }
        private ILogger Logger { get; }
        private IProjectRepository ProjectRepository { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public O105B_AddServiceImplementationsToRepository(
            IHumanOutput humanOutput,
            ILogger<O105B_AddServiceImplementationsToRepository> logger,
            IProjectRepository projectRepository,
            Repositories.IServiceRepository serviceRepository)
        {
            this.HumanOutput = humanOutput;
            this.Logger = logger;
            this.ProjectRepository = projectRepository;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run(
            IList<ServiceImplementationDescriptor> serviceImplementationDescriptors)
        {
            this.Logger.LogInformation($"Starting operation {nameof(O105B_AddServiceImplementationsToRepository)}...");

            var serviceImplementationDescriptorsDistinctByNamespacedTypeName = serviceImplementationDescriptors.GetDistinct(
                NamedEqualityComparer<ServiceImplementationDescriptor>.Instance);

            /// Service implementations.
            {
                var currentServiceImplementationDescriptors = serviceImplementationDescriptorsDistinctByNamespacedTypeName
                    .Select(x => new ServiceComponentDescriptor
                    {
                        CodeFilePath = x.CodeFilePath,
                        ProjectFilePath = x.ProjectFilePath,
                        TypeName = x.NamespacedTypeName,
                    })
                    .Now();

                this.Logger.LogInformation("Getting all repository service implementations...");

                var initialRepositoryServiceImplementations = await this.ServiceRepository.GetAllServiceImplementations();

                var repositoryServiceImplementationsCount = initialRepositoryServiceImplementations.Length;
                this.HumanOutput.WriteLine($"Repository contains {repositoryServiceImplementationsCount} service implementations.");

                this.Logger.LogInformation("Determining new and departed service implementations...");

                // Verify data is distinct by (type name-code file path) pair.
                var duplicateCurrentServiceImplementationDescriptors = currentServiceImplementationDescriptors.GetDuplicateNamedFilePathedSets();
                if(duplicateCurrentServiceImplementationDescriptors.Any())
                {
                    throw new Exception("Duplicate current service implementation descriptors found.");
                }

                initialRepositoryServiceImplementations.VerifyDistinctByNamedFilePathedData();

                // Find new service definitions.
                var newServiceImplementationDescriptors = currentServiceImplementationDescriptors.Except(
                    initialRepositoryServiceImplementations,
                    NamedFilePathedEqualityComparer.Instance)
                    .Cast<IServiceComponentDescriptor>()
                    .Now();

                var newServiceImplementationDescriptorsCount = newServiceImplementationDescriptors.Length;
                this.HumanOutput.WriteLine($"Found {newServiceImplementationDescriptorsCount} new service implementations.");

                // Find departed service definitions.
                var departedServiceImplementations = initialRepositoryServiceImplementations.Except(
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
            }

            /// Repository implementations.
            // TODO: should be a unique selection by name, perhaps at a per-client project level (using available assembly information), but for now just fake it.
            var allRepositoryServiceImplementations = await this.ServiceRepository.GetAllServiceImplementations();

            var repositoryServiceImplementations = allRepositoryServiceImplementations.GetDistinct(
                NamedEqualityComparer<Repositories.ServiceImplementation>.Instance);

            var duplicateRepositoryServiceImplementations = repositoryServiceImplementations.GetDuplicateNameSets();
            if(duplicateRepositoryServiceImplementations.Any())
            {
                throw new Exception("Service implementations with duplicate type names were found in the repository.");
            }

            var repositoryServiceImplementationsByNamespacedTypeName = repositoryServiceImplementations
                .ToDictionary(
                    x => x.TypeName);

            /// To-project mappings.
            {
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

                var projects = await this.ProjectRepository.GetAllProjects();

                var projectsByProjectFilePath = projects.ToDictionaryByFilePath();

                var currentProjectsByServiceImplementation = serviceImplementationDescriptorsDistinctByNamespacedTypeName
                    .ToDictionary(
                        x => repositoryServiceImplementationsByNamespacedTypeName[x.NamespacedTypeName],
                        x => projectsByProjectFilePath[x.ProjectFilePath]);

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
            }

            /// Service definitions implemented by service implementations.
            var allDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            // Verify. Can disaggregate service definitions with the same namespaced type name using to-project mapping information, but that is lots of extra work!
            // For now just error.
            // TODO: there should be a unique service definition selection process, but fake it for now.

            var definitions = allDefinitions
                .GetDistinct(
                    NamedEqualityComparer<Repositories.ServiceDefinition>.Instance)
                .Now();

            var duplicateServiceDefinitionByNamespacedTypeNames = definitions.GetDuplicateNameSets().Now();
            if (duplicateServiceDefinitionByNamespacedTypeNames.Any())
            {
                throw new Exception("Duplicate service definition namespaced type names found.");
            }

            var definitionsByNamespacedTypeName = definitions
                .ToDictionary(
                    x => x.TypeName);

            {
                this.Logger.LogInformation("Determining new and departed service implementation-to-implemented definition mappings...");

                var currentImplementationAndDefinitionPairs = serviceImplementationDescriptorsDistinctByNamespacedTypeName
                    .Where(x => x.HasServiceDefinition)
                    .Select(x => new Repositories.ImplementedDefinitionMapping
                    {
                        Definition = definitionsByNamespacedTypeName[x.ServiceDefinitionNamespacedTypeName],
                        Implementation = repositoryServiceImplementationsByNamespacedTypeName[x.NamespacedTypeName],
                    })
                    .Now();

                // Get repository definitions-by-implementation.
                var respositoryImplementationAndDefinitionPairs = await this.ServiceRepository.GetAllImplementedDefinitionMappings();

                // Use a comparer to find new/departed definitions-by-implementation pair.
                var departedImplementedDefinitionMappings = respositoryImplementationAndDefinitionPairs.Except(
                        currentImplementationAndDefinitionPairs,
                        Repositories.IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer<Repositories.ImplementedDefinitionMapping>.Instance)
                    .Now();

                var newImplementedDefinitionMappings = currentImplementationAndDefinitionPairs.Except(
                        respositoryImplementationAndDefinitionPairs,
                        Repositories.IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer< Repositories.ImplementedDefinitionMapping>.Instance)
                    .Now();

                var departedImplementedDefinitionMappingsCount = departedImplementedDefinitionMappings.Length;
                this.HumanOutput.WriteLine($"Found {departedImplementedDefinitionMappingsCount} departed service implementation-to-implemented definition mappings.");

                var newImplementedDefinitionMappingsCount = newImplementedDefinitionMappings.Length;
                this.HumanOutput.WriteLine($"Found {newImplementedDefinitionMappingsCount} new service implementation-to-implemented definition mappings.");

                // Delete first (in case any mappings have changed, so we don't violate the uniqueness constraint on implementation).
                this.Logger.LogInformation("Removing departed service implementation-to-implemented definition mappings from the repository...");

                await this.ServiceRepository.DeleteImplementedDefinitions(departedImplementedDefinitionMappings);

                // Then add new.
                this.Logger.LogInformation("Adding new service implementation-to-implemented definition mappings to the repository...");

                await this.ServiceRepository.AddImplementedDefinitions(newImplementedDefinitionMappings);
            }

            /// Service dependencies.
            {
                var dependencyMappings = serviceImplementationDescriptorsDistinctByNamespacedTypeName
                    .Where(x => x.HasServiceDependencies)
                    .SelectMany(x => x.ServiceDependencyNamespacedTypeNames
                        .Select(y => new Repositories.DependencyDefinitionMapping
                        {
                            Definition = definitionsByNamespacedTypeName[y],
                            Implementation = repositoryServiceImplementationsByNamespacedTypeName[x.NamespacedTypeName],
                        }))
                    .Now();

                var repositoryDependencyMappings = await this.ServiceRepository.GetAllDependencyDefinitionMappings();

                var nulls = repositoryDependencyMappings
                    .Where(x => x.Definition is null || x.Implementation is null)
                    .Now();

                var departedDependencyDefinitionMappings = repositoryDependencyMappings.Except(
                    dependencyMappings,
                    Repositories.IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer<Repositories.DependencyDefinitionMapping>.Instance)
                    .Now();

                var newDependencyDefinitionMappings = dependencyMappings.Except(
                    repositoryDependencyMappings,
                    Repositories.IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer<Repositories.DependencyDefinitionMapping>.Instance)
                    .Now();

                var departedImplementedDefinitionMappingsCount = departedDependencyDefinitionMappings.Length;
                this.HumanOutput.WriteLine($"Found {departedImplementedDefinitionMappingsCount} departed service implementation-to-dependency definition mappings.");

                var newImplementedDefinitionMappingsCount = newDependencyDefinitionMappings.Length;
                this.HumanOutput.WriteLine($"Found {newImplementedDefinitionMappingsCount} new service implementation-to-dependency definition mappings.");

                // Delete first (in case any mappings have changed, so we don't violate the uniqueness constraint on implementation).
                this.Logger.LogInformation("Removing departed service implementation-to-dependency definition mappings from the repository...");

                await this.ServiceRepository.DeleteDependencyDefinitions(departedDependencyDefinitionMappings);

                // Then add new.
                this.Logger.LogInformation("Adding new service implementation-to-dependency definition mappings to the repository...");

                await this.ServiceRepository.AddDependencyDefinitions(newDependencyDefinitionMappings);
            }

            this.Logger.LogInformation($"Finished operation {nameof(O105B_AddServiceImplementationsToRepository)}.");
        }
    }
}
