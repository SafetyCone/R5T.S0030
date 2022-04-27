using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.T0064;
using R5T.T0094;
using R5T.T0097;
using R5T.T0101;
using R5T.T0128;
using R5T.T0128.D001;


namespace R5T.S0030.Repositories
{
    [ServiceImplementationMarker]
    public class ServiceRepository<TFileContext> : FileContextBasedRepositoryBase<TFileContext>, IServiceRepository, T0064.IServiceImplementation
        where TFileContext : FileContext, FileContexts.IServiceRepositoryFileContext
    {
        public ServiceRepository(
            IFileContextProvider<TFileContext> fileContextProvider)
            : base(fileContextProvider)
        {
        }

        public async Task AddDependencyDefinitions(IEnumerable<DependencyDefinitionMapping> mappings)
        {
            // Verify all identities are set.
            var serviceDefinitions = mappings
                .Select(x => x.Definition)
                .Now();

            serviceDefinitions.VerifyAllIdentitiesAreSet();

            var serviceImplementations = mappings
                .Select(x => x.Implementation)
                .Now();

            serviceImplementations.VerifyAllIdentitiesAreSet();

            // Non-idempotent: throw if any service implementations already have definitions.
            var hasDefinitions = await this.HasDependencyDefinitions(serviceImplementations);

            var anyImplementationsHaveDefinitions = hasDefinitions.AnyWereFound();
            if (anyImplementationsHaveDefinitions)
            {
                throw new Exception("Some service implementations were already mapped to service dependency definitions.");
            }

            // Create mappings.
            var mappingEntities = mappings
                .Select(x => new FileContexts.Entities.ImplementionToDefinitionMapping
                {
                    DefinitionIdentity = x.Definition.Identity,
                    ImplementationIdentity = x.Implementation.Identity,
                })
                .Now();

            // Add mappings.
            await this.ExecuteInContext(async fileContext =>
            {
                fileContext.ToDependencyDefinitionMappings.AddRange(mappingEntities);

                await fileContext.Save();
            });
        }

        public async Task AddImplementedDefinitions(IEnumerable<ImplementedDefinitionMapping> mappings)
        {
            // Verify all identities are set.
            var serviceDefinitions = mappings
                .Select(x => x.Definition)
                .Now();

            serviceDefinitions.VerifyAllIdentitiesAreSet();

            var serviceImplementations = mappings
                .Select(x => x.Implementation)
                .Now();

            serviceImplementations.VerifyAllIdentitiesAreSet();

            // Non-idempotent: throw if any service implementations already have definitions.
            var hasDefinitions = await this.HasImplementedDefinitions(serviceImplementations);

            var anyImplementationsHaveDefinitions = hasDefinitions.AnyWereFound();
            if (anyImplementationsHaveDefinitions)
            {
                throw new Exception("Some service implementations were already mapped to service definitions.");
            }

            // Create mappings.
            var mappingEntities = mappings
                .Select(x => new FileContexts.Entities.ImplementionToDefinitionMapping
                {
                    DefinitionIdentity = x.Definition.Identity,
                    ImplementationIdentity = x.Implementation.Identity,
                })
                .Now();

            // Add mappings.
            await this.ExecuteInContext(async fileContext =>
            {
                fileContext.ToImplementedDefinitionMappings.AddRange(mappingEntities);

                await fileContext.Save();
            });
        }

        public async Task AddProjectsForServiceDefinitions(
            IEnumerable<(ServiceDefinition serviceDefinition, Project project)> instancePairs)
        {
            // Verify inputs.
            var serviceDefinitions = instancePairs
                .Select(x => x.serviceDefinition)
                .Now();

            serviceDefinitions.VerifyAllIdentitiesAreSet();

            var projects = instancePairs
                .Select(x => x.project)
                .Now();

            projects.VerifyAllIdentitiesAreSet();

            // Non-idempotent: throw if any service definitions already have projects.
            var hasProjects = await this.HasProjectsForServiceDefinitions(serviceDefinitions);

            var anyServiceDefinitionsHaveProjects = hasProjects.AnyWereFound();
            if(anyServiceDefinitionsHaveProjects)
            {
                throw new Exception("Some service definitions were already mapped to projects.");
            }

            // Create mappings.
            var toProjectMappingEntities = instancePairs
                .Select(x => new FileContexts.Entities.ServiceComponentToProjectMapping
                {
                    ProjectIdentity = x.project.Identity,
                    ServiceComponentIdentity = x.serviceDefinition.Identity,
                })
                .Now();

            // Add mappings.
            await this.ExecuteInContext(async fileContext =>
            {
                fileContext.ServiceComponentToProjectMappings.AddRange(toProjectMappingEntities);

                await fileContext.Save();
            });
        }

        public async Task AddProjectsForServiceImplementations(
            IEnumerable<(ServiceImplementation serviceImplementation, Project project)> instancePairs)
        {
            // Verify inputs.
            var serviceImplementations = instancePairs
                .Select(x => x.serviceImplementation)
                .Now();

            serviceImplementations.VerifyAllIdentitiesAreSet();

            var projects = instancePairs
                .Select(x => x.project)
                .Now();

            projects.VerifyAllIdentitiesAreSet();

            // Non-idempotent: throw if any service definitions already have projects.
            var hasProjects = await this.HasProjectsForServiceImplementations(serviceImplementations);

            var anyServiceImplementationsHaveProjects = hasProjects.AnyWereFound();
            if (anyServiceImplementationsHaveProjects)
            {
                throw new Exception("Some service implementations were already mapped to projects.");
            }

            // Create mappings.
            var toProjectMappingEntities = instancePairs
                .Select(x => new FileContexts.Entities.ServiceComponentToProjectMapping
                {
                    ProjectIdentity = x.project.Identity,
                    ServiceComponentIdentity = x.serviceImplementation.Identity,
                })
                .Now();

            // Add mappings.
            await this.ExecuteInContext(async fileContext =>
            {
                fileContext.ServiceComponentToProjectMappings.AddRange(toProjectMappingEntities);

                await fileContext.Save();
            });
        }

        public async Task<Dictionary<Guid, ServiceDefinition>> AddServiceDefinitions(IEnumerable<ServiceComponentDataSet> dataSets)
        {
            // Determine if any of the service definitions exist by data-set. If they do, throw.
            var hasDataSets = await this.HasServiceDefinitions(dataSets);

            var anyDataSetsAlreadyExist = hasDataSets.AnyTrueValues();
            if(anyDataSetsAlreadyExist)
            {
                throw new Exception("Some service definitions already existed.");
            }

            // Now add.
            var output = await this.ExecuteInContext(async fileContext =>
            {
                var serviceDefinitions = dataSets
                    .Select(x => x.GetServiceDefinition(
                        Instances.GuidOperator.NewGuid()))
                    .ToDictionary(x => x.Identity);

                var entities = serviceDefinitions.Values
                    .Select(x => x.ToEntityType())
                    ;

                fileContext.ServiceDefinitions.AddRange(entities);

                await fileContext.Save();

                return serviceDefinitions;
            });

            return output;
        }

        public async Task<Dictionary<Guid, ServiceImplementation>> AddServiceImplementations(IEnumerable<ServiceComponentDataSet> dataSets)
        {
            // Determine if any of the service definitions exist by data-set. If they do, throw.
            var hasDataSets = await this.HasServiceImplementations(dataSets);

            var anyDataSetsAlreadyExist = hasDataSets.AnyTrueValues();
            if (anyDataSetsAlreadyExist)
            {
                throw new Exception("Some service implementations already existed.");
            }

            // Now add.
            var output = await this.ExecuteInContext(async fileContext =>
            {
                var serviceImplementations = dataSets
                    .Select(x => x.GetServiceImplementation(
                        Instances.GuidOperator.NewGuid()))
                    .ToDictionary(x => x.Identity);

                var entities = serviceImplementations.Values
                    .Select(x => x.ToEntityType())
                    ;

                fileContext.ServiceImplementations.AddRange(entities);

                await fileContext.Save();

                return serviceImplementations;
            });

            return output;
        }

        public async Task<Dictionary<DependencyDefinitionMapping, bool>> DeleteDependencyDefinitions(IEnumerable<DependencyDefinitionMapping> mappings)
        {
            // Verify all identities are set.
            var serviceDefinitions = mappings
                .Select(x => x.Definition)
                .Now();

            serviceDefinitions.VerifyAllIdentitiesAreSet();

            var serviceImplementations = mappings
                .Select(x => x.Implementation)
                .Now();

            serviceImplementations.VerifyAllIdentitiesAreSet();

            // Now delete.
            var output = await this.ExecuteInContext(async fileContext =>
            {
                // Build target hash.
                var mappingEntitiesHash = mappings
                    .Select(x => new FileContexts.Entities.ImplementionToDefinitionMapping
                    {
                        DefinitionIdentity = x.Definition.Identity,
                        ImplementationIdentity = x.Implementation.Identity,
                    })
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ImplementionToDefinitionMapping>.Instance);

                // Remove.
                var toRemoveMappingEntities = fileContext.ToDependencyDefinitionMappings
                    .Where(x => mappingEntitiesHash.Contains(x))
                    .Now();

                fileContext.ToDependencyDefinitionMappings.RemoveRange(toRemoveMappingEntities);

                await fileContext.Save();

                // Build output.
                var toRemoveMappingEntitiesHash = toRemoveMappingEntities
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ImplementionToDefinitionMapping>.Instance);

                var output = mappings.ToDictionary(
                    x => x,
                    x => toRemoveMappingEntitiesHash.Contains(new FileContexts.Entities.ImplementionToDefinitionMapping
                    {
                        DefinitionIdentity = x.Definition.Identity,
                        ImplementationIdentity = x.Implementation.Identity,
                    }));

                return output;
            });

            return output;
        }

        public async Task<Dictionary<ImplementedDefinitionMapping, bool>> DeleteImplementedDefinitions(IEnumerable<ImplementedDefinitionMapping> mappings)
        {
            // Verify all identities are set.
            var serviceDefinitions = mappings
                .Select(x => x.Definition)
                .Now();

            serviceDefinitions.VerifyAllIdentitiesAreSet();

            var serviceImplementations = mappings
                .Select(x => x.Implementation)
                .Now();

            serviceImplementations.VerifyAllIdentitiesAreSet();

            // Now delete.
            var output = await this.ExecuteInContext(async fileContext =>
            {
                // Build target hash.
                var mappingEntitiesHash = mappings
                    .Select(x => new FileContexts.Entities.ImplementionToDefinitionMapping
                    {
                        DefinitionIdentity = x.Definition.Identity,
                        ImplementationIdentity = x.Implementation.Identity,
                    })
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ImplementionToDefinitionMapping>.Instance);

                // Remove.
                var toRemoveMappingEntities = fileContext.ToImplementedDefinitionMappings
                    .Where(x => mappingEntitiesHash.Contains(x))
                    .Now();

                fileContext.ToImplementedDefinitionMappings.RemoveRange(toRemoveMappingEntities);

                await fileContext.Save();

                // Build output.
                var toRemoveMappingEntitiesHash = toRemoveMappingEntities
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ImplementionToDefinitionMapping>.Instance);

                var output = mappings.ToDictionary(
                    x => x,
                    x => toRemoveMappingEntitiesHash.Contains(new FileContexts.Entities.ImplementionToDefinitionMapping
                    {
                        DefinitionIdentity = x.Definition.Identity,
                        ImplementationIdentity = x.Implementation.Identity,
                    }));

                return output;
            });

            return output;
        }

        public async Task<Dictionary<ServiceDefinition, bool>> DeleteProjectsForServiceDefinitions(
            IEnumerable<(ServiceDefinition serviceDefinition, Project project)> instancePairs)
        {
            // Verify inputs.
            var serviceDefinitions = instancePairs
                .Select(x => x.serviceDefinition)
                .Now();

            serviceDefinitions.VerifyAllIdentitiesAreSet();

            var projects = instancePairs
                .Select(x => x.project)
                .Now();

            projects.VerifyAllIdentitiesAreSet();

            // Now delete.
            var output = await this.ExecuteInContext(async fileContext =>
            {
                // Remove.
                var toRemoveToProjectMappingEntitiesHash = instancePairs
                    .Select(x => new FileContexts.Entities.ServiceComponentToProjectMapping
                    {
                        ProjectIdentity = x.project.Identity,
                        ServiceComponentIdentity = x.serviceDefinition.Identity,
                    })
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ServiceComponentToProjectMapping>.Instance);

                var toProjectMappingEntitiesToRemove = fileContext.ServiceComponentToProjectMappings
                    .Where(x => toRemoveToProjectMappingEntitiesHash.Contains(x))
                    .Now();

                fileContext.ServiceComponentToProjectMappings.RemoveRange(toProjectMappingEntitiesToRemove);

                await fileContext.Save();

                // Build output.
                var toProjectMappingEntitiesToRemoveHash = toProjectMappingEntitiesToRemove
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ServiceComponentToProjectMapping>.Instance);

                var output = instancePairs.ToDictionary(
                    x => x.serviceDefinition,
                    x => toProjectMappingEntitiesToRemoveHash.Contains(new FileContexts.Entities.ServiceComponentToProjectMapping
                    {
                        ProjectIdentity = x.project.Identity,
                        ServiceComponentIdentity = x.serviceDefinition.Identity,
                    }));

                return output;
            });

            return output;
        }

        public async Task<Dictionary<ServiceImplementation, bool>> DeleteProjectsForServiceImplementations(
            IEnumerable<(ServiceImplementation serviceImplementation, Project project)> instancePairs)
        {
            // Verify inputs.
            var serviceImplementations = instancePairs
                .Select(x => x.serviceImplementation)
                .Now();

            serviceImplementations.VerifyAllIdentitiesAreSet();

            var projects = instancePairs
                .Select(x => x.project)
                .Now();

            projects.VerifyAllIdentitiesAreSet();

            // Now delete.
            var output = await this.ExecuteInContext(async fileContext =>
            {
                // Remove.
                var toRemoveToProjectMappingEntitiesHash = instancePairs
                    .Select(x => new FileContexts.Entities.ServiceComponentToProjectMapping
                    {
                        ProjectIdentity = x.project.Identity,
                        ServiceComponentIdentity = x.serviceImplementation.Identity,
                    })
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ServiceComponentToProjectMapping>.Instance);

                var toProjectMappingEntitiesToRemove = fileContext.ServiceComponentToProjectMappings
                    .Where(x => toRemoveToProjectMappingEntitiesHash.Contains(x))
                    .Now();

                fileContext.ServiceComponentToProjectMappings.RemoveRange(toProjectMappingEntitiesToRemove);

                await fileContext.Save();

                // Build output.
                var toProjectMappingEntitiesToRemoveHash = toProjectMappingEntitiesToRemove
                    .ToHashSet(IdentityMappingEqualityComparer<FileContexts.Entities.ServiceComponentToProjectMapping>.Instance);

                var output = instancePairs.ToDictionary(
                    x => x.serviceImplementation,
                    x => toProjectMappingEntitiesToRemoveHash.Contains(new FileContexts.Entities.ServiceComponentToProjectMapping
                    {
                        ProjectIdentity = x.project.Identity,
                        ServiceComponentIdentity = x.serviceImplementation.Identity,
                    }));

                return output;
            });

            return output;
        }

        public async Task<Dictionary<Guid, bool>> DeleteServiceDefinitions(IEnumerable<Guid> identities)
        {
            var identitiesHash = identities.ToHashSet();

            var output = await this.ExecuteInContext(async fileContext =>
            {
                var entitiesToRemove = fileContext.ServiceDefinitions
                    .Where(x => identitiesHash.Contains(x.Identity))
                    .Now();

                fileContext.ServiceDefinitions.RemoveRange(entitiesToRemove);

                await fileContext.Save();

                var entitiesToRemoveIdentitiesHash = entitiesToRemove
                    .Select(x => x.Identity)
                    .ToHashSet();

                var output = identitiesHash
                    .Select(x => new { Identity = x, Exists = entitiesToRemoveIdentitiesHash.Contains(x) })
                    .ToDictionary(
                        x => x.Identity,
                        x => x.Exists);

                return output;
            });

            return output;
        }

        public async Task<Guid[]> DeleteServiceDefinitions(Func<IServiceDefinition, bool> predicate)
        {
            var output = await this.ExecuteInContext(async fileContext =>
            {
                var entitiesToRemove = fileContext.ServiceDefinitions
                    .Where(predicate)
                    .Cast<FileContexts.Entities.ServiceDefinition>()
                    .Now();

                fileContext.ServiceDefinitions.RemoveRange(entitiesToRemove);

                await fileContext.Save();

                var output = entitiesToRemove
                    .Select(x => x.Identity)
                    .Now();

                return output;
            });

            return output;
        }

        public async Task<Dictionary<Guid, bool>> DeleteServiceImplementations(IEnumerable<Guid> identities)
        {
            var identitiesHash = identities.ToHashSet();

            var output = await this.ExecuteInContext(async fileContext =>
            {
                var entitiesToRemove = fileContext.ServiceImplementations
                    .Where(x => identitiesHash.Contains(x.Identity))
                    .Now();

                fileContext.ServiceImplementations.RemoveRange(entitiesToRemove);

                await fileContext.Save();

                var entitiesToRemoveIdentitiesHash = entitiesToRemove
                    .Select(x => x.Identity)
                    .ToHashSet();

                var output = identitiesHash
                    .Select(x => new { Identity = x, Exists = entitiesToRemoveIdentitiesHash.Contains(x) })
                    .ToDictionary(
                        x => x.Identity,
                        x => x.Exists);

                return output;
            });

            return output;
        }

        public async Task<DependencyDefinitionMapping[]> GetAllDependencyDefinitionMappings()
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var definitionAndImplementationEntityPairs =
                    from mapping in fileContext.ToDependencyDefinitionMappings
                    join definition in fileContext.ServiceDefinitions on mapping.DefinitionIdentity equals definition.Identity into definitions
                    from definition in definitions.DefaultIfEmpty()
                    join implementation in fileContext.ServiceImplementations on mapping.ImplementationIdentity equals implementation.Identity into implementations
                    from implementation in implementations.DefaultIfEmpty()
                    select (Definition: definition, Implementation: implementation)
                    ;

                var output = definitionAndImplementationEntityPairs
                    .Select(x => new DependencyDefinitionMapping
                    {
                        Definition = x.Definition?.ToAppType(),
                        Implementation = x.Implementation?.ToAppType(),
                    })
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<ImplementedDefinitionMapping[]> GetAllImplementedDefinitionMappings()
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var definitionAndImplementationEntityPairs =
                    from mapping in fileContext.ToImplementedDefinitionMappings
                    join definition in fileContext.ServiceDefinitions on mapping.DefinitionIdentity equals definition.Identity into definitions
                    from definition in definitions.DefaultIfEmpty()
                    join implementation in fileContext.ServiceImplementations on mapping.ImplementationIdentity equals implementation.Identity into implementations
                    from implementation in implementations.DefaultIfEmpty()
                    select (Definition: definition, Implementation: implementation)
                    ;

                var output = definitionAndImplementationEntityPairs
                    .Select(x => new ImplementedDefinitionMapping
                    {
                        Definition = x.Definition.ToAppType(),
                        Implementation = x.Implementation.ToAppType(),
                    })
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<(ServiceDefinition serviceDefinition, Project project)[]> GetAllServiceDefinitionToProjectMappings()
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceDefinitionsByIdentity = fileContext.ServiceDefinitions.ToDictionaryByIdentity();
                var projectsByIdentity = fileContext.Projects.ToDictionaryByIdentity();

                var output = fileContext.ServiceComponentToProjectMappings
                    .Where(x => serviceDefinitionsByIdentity.ContainsKey(x.ServiceComponentIdentity))
                    .Select(x => (
                        serviceDefinitionsByIdentity[x.ServiceComponentIdentity].ToAppType(),
                        projectsByIdentity[x.ProjectIdentity].ToAppType()))
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<(ServiceImplementation serviceImplementation, Project project)[]> GetAllServiceImplementationToProjectMappings()
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceImplementationsByIdentity = fileContext.ServiceImplementations.ToDictionaryByIdentity();
                var projectsByIdentity = fileContext.Projects.ToDictionaryByIdentity();

                var output = fileContext.ServiceComponentToProjectMappings
                    .Where(x => serviceImplementationsByIdentity.ContainsKey(x.ServiceComponentIdentity))
                    .Select(x => (
                        serviceImplementationsByIdentity[x.ServiceComponentIdentity].ToAppType(),
                        projectsByIdentity[x.ProjectIdentity].ToAppType()))
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<ServiceDefinition[]> GetServiceDefinitions(Func<IServiceDefinition, bool> predicate)
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var output = fileContext.ServiceDefinitions
                    .Where(predicate)
                    .Cast<FileContexts.Entities.ServiceDefinition>()
                    .Select(x => x.ToAppType())
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<TValue[]> GetServiceDefinitionValues<TValue>(Func<IServiceDefinition, bool> predicate, Func<IServiceDefinition, TValue> selector)
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var output = fileContext.ServiceDefinitions
                    .Where(predicate)
                    .Select(selector)
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<ServiceImplementation[]> GetServiceImplementations(Func<IServiceImplementation, bool> predicate)
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var output = fileContext.ServiceImplementations
                    .Where(predicate)
                    .Cast<FileContexts.Entities.ServiceImplementation>()
                    .Select(x => x.ToAppType())
                    .Now();

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<IServiceImplementationDataIdentity, WasFound<ServiceDefinition[]>>> HasDependencyDefinitions(
            IEnumerable<IServiceImplementationDataIdentity> implementationDataIdentities)
        {
            var distinctImplementationDataIdentities = implementationDataIdentities
                    .Distinct(NamedFilePathedEqualityComparer<IServiceImplementationDataIdentity>.Instance)
                    .Now();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceDefinitionAndServiceImplementationDataIdentityPairs = distinctImplementationDataIdentities
                    // Get entity-types for app-types: left-outer join between service implementation app-type and entity-type, using an equality comparer instance.
                    .GroupJoin(fileContext.ServiceImplementations,
                        x => x,
                        x => x,
                        (x, y) => (ServiceImplementationDataIdentity: x, ServiceImplementations: y),
                        NamedFilePathedEqualityComparer.Instance)
                    .SelectMany(
                        x => x.ServiceImplementations.DefaultIfEmpty(),
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceImplementationIdentity: y?.Identity ?? default))
                    // Get definition identity for dependencies: left-outer join between prior join and to-implemented definition mappings.
                    .GroupJoin(fileContext.ToDependencyDefinitionMappings,
                        x => x.ServiceImplementationIdentity,
                        x => x.ImplementationIdentity,
                        (x, y) => (x.ServiceImplementationDataIdentity, ToImplementedDefinitionMappings: y))
                    .SelectMany(
                        x => x.ToImplementedDefinitionMappings.DefaultIfEmpty(),
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceDefinitionIdentity: y?.DefinitionIdentity ?? default))
                    // Get the service definition for the definition identity: left-outer join between prior join and service definitions.
                    .GroupJoin(fileContext.ServiceDefinitions,
                        x => x.ServiceDefinitionIdentity,
                        y => y.Identity,
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceDefinitions: y))
                    .SelectMany(
                        x => x.ServiceDefinitions.DefaultIfEmpty(),
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceDefinition: y))
                    ;

                var output = serviceDefinitionAndServiceImplementationDataIdentityPairs
                    .GroupBy(
                        x => x.ServiceImplementationDataIdentity)
                    .ToDictionary(
                        x => x.Key,
                        x => WasFound.FromArray(x
                            // We want empty arrays, not arrays of a single default object.
                            .Where(x => x.ServiceDefinition is object)
                            .Select(x => x.ServiceDefinition.ToAppType())
                            .ToArray()));

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<IServiceImplementationDataIdentity, WasFound<ServiceDefinition>>> HasImplementedDefinitions(
            IEnumerable<IServiceImplementationDataIdentity> implementationDataIdentities)
        {
            var distinctImplementationDataIdentities = implementationDataIdentities
                    .Distinct(NamedFilePathedEqualityComparer<IServiceImplementationDataIdentity>.Instance)
                    .Now();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceDefinitionAndServiceImplementationDataIdentityPairs = distinctImplementationDataIdentities
                    // Get entity-types for app-types: left-outer join between service implementation app-type and entity-type, using an equality comparer instance.
                    .GroupJoin(fileContext.ServiceImplementations,
                        x => x,
                        x => x,
                        (x, y) => (ServiceImplementationDataIdentity: x, ServiceImplementations: y),
                        NamedFilePathedEqualityComparer.Instance)
                    .SelectMany(
                        x => x.ServiceImplementations.DefaultIfEmpty(),
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceImplementationIdentity: y?.Identity ?? default))
                    // Get definition identity for implementation: left-outer join between prior join and to-implemented definition mappings.
                    .GroupJoin(fileContext.ToImplementedDefinitionMappings,
                        x => x.ServiceImplementationIdentity,
                        x => x.ImplementationIdentity,
                        (x, y) => (x.ServiceImplementationDataIdentity, ToImplementedDefinitionMappings: y))
                    .SelectMany(
                        x => x.ToImplementedDefinitionMappings.DefaultIfEmpty(),
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceDefinitionIdentity: y?.DefinitionIdentity ?? default))
                    // Get the service definition for the definition identity: left-outer join between prior join and service definitions.
                    .GroupJoin(fileContext.ServiceDefinitions,
                        x => x.ServiceDefinitionIdentity,
                        y => y.Identity,
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceDefinitions: y))
                    .SelectMany(
                        x => x.ServiceDefinitions.DefaultIfEmpty(),
                        (x, y) => (x.ServiceImplementationDataIdentity, ServiceDefinition: y))
                    ;

                var output = serviceDefinitionAndServiceImplementationDataIdentityPairs
                    .ToDictionary(
                        x => x.ServiceImplementationDataIdentity,
                        x => x.ServiceDefinition?.ToAppType().WasFound() ?? WasFound.NotFound<ServiceDefinition>());

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<ServiceDefinition, WasFound<Project>>> HasProjectsForServiceDefinitions(
            IEnumerable<ServiceDefinition> serviceDefinitions)
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var toProjectMappingsByServiceComponentIdentity = fileContext.ServiceComponentToProjectMappings.ToDictionaryByLocalIdentity();

                var toProjectMappingsServiceComponentIdentitiesHash = toProjectMappingsByServiceComponentIdentity.Keys.ToHashSet();

                var serviceDefintionsWithoutProject = serviceDefinitions
                    .Where(x => !toProjectMappingsServiceComponentIdentitiesHash.Contains(x.Identity))
                    .Now();

                var projectsByProjectIdentity = fileContext.Projects.ToDictionaryByIdentity();

                var output = serviceDefinitions
                    .Join(toProjectMappingsByServiceComponentIdentity,
                        x => x.Identity,
                        x => x.Key,
                        (x, y) => (ServiceDefinition: x, ToProjectMapping: y.Value))
                    .Join(projectsByProjectIdentity,
                        x => x.ToProjectMapping.ProjectIdentity,
                        y => y.Key,
                        (x, y) => (x.ServiceDefinition, Project: y.Value))
                    .Select(x => (x.ServiceDefinition, WasFound: WasFound.Found(x.Project.ToAppType())))
                    .Concat(serviceDefintionsWithoutProject
                        .Select(x => (ServiceDefinition: x, WasFound: WasFound.NotFound<Project>())))
                    .ToDictionary(
                        x => x.ServiceDefinition,
                        x => x.WasFound);

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<ServiceImplementation, WasFound<Project>>> HasProjectsForServiceImplementations(
            IEnumerable<ServiceImplementation> serviceImplementations)
        {
            var output = await this.ExecuteInContext(fileContext =>
            {
                var toProjectMappingsByServiceComponentIdentity = fileContext.ServiceComponentToProjectMappings.ToDictionaryByLocalIdentity();

                var toProjectMappingsServiceComponentIdentitiesHash = toProjectMappingsByServiceComponentIdentity.Keys.ToHashSet();

                var serviceImplementationsWithoutProject = serviceImplementations
                    .Where(x => !toProjectMappingsServiceComponentIdentitiesHash.Contains(x.Identity))
                    .Now();

                var projectsByProjectIdentity = fileContext.Projects.ToDictionaryByIdentity();

                var output = serviceImplementations
                    .Join(toProjectMappingsByServiceComponentIdentity,
                        x => x.Identity,
                        x => x.Key,
                        (x, y) => (ServiceImplementation: x, ToProjectMapping: y.Value))
                    .Join(projectsByProjectIdentity,
                        x => x.ToProjectMapping.ProjectIdentity,
                        y => y.Key,
                        (x, y) => (x.ServiceImplementation, Project: y.Value))
                    .Select(x => (x.ServiceImplementation, WasFound: WasFound.Found(x.Project.ToAppType())))
                    .Concat(serviceImplementationsWithoutProject
                        .Select(x => (ServiceImplementation: x, WasFound: WasFound.NotFound<Project>())))
                    .ToDictionary(
                        x => x.ServiceImplementation,
                        x => x.WasFound);

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<Guid, WasFound<ServiceDefinition>>> HasServiceDefinitions(IEnumerable<Guid> identities)
        {
            var identitiesHash = identities.ToHashSet();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceDefinitions = fileContext.ServiceDefinitions
                    .Where(x => identitiesHash.Contains(x.Identity))
                    .Now();

                var foundIdentities = serviceDefinitions
                    .Select(x => x.Identity)
                    ;

                var notFoundIdentities = identitiesHash.Except(foundIdentities);

                var output = serviceDefinitions
                    .Select(x => (x.Identity, WasFound: WasFound.From(x.ToAppType())))
                    .Concat(notFoundIdentities
                        .Select(x => (Identity: x, WasFound: WasFound.NotFound<ServiceDefinition>())))
                    .ToDictionary(
                        x => x.Identity,
                        x => x.WasFound);

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<ITypeNamedCodeFilePathed, WasFound<ServiceDefinition>>> HasServiceDefinitions(IEnumerable<ITypeNamedCodeFilePathed> typeNamedCodeFilePatheds)
        {
            typeNamedCodeFilePatheds.VerifyDistinctByNamedFilePathedData();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceDefinitionsByServiceDefinition = fileContext.ServiceDefinitions.ToDictionary(
                    x => x as ITypeNamedCodeFilePathed,
                    NamedFilePathedEqualityComparer<ITypeNamedCodeFilePathed>.Instance);

                var output = typeNamedCodeFilePatheds
                    .Select(x =>
                    {
                        var wasFound = serviceDefinitionsByServiceDefinition.ContainsKey(x)
                            ? WasFound.From(serviceDefinitionsByServiceDefinition[x].ToAppType())
                            : WasFound.NotFound<ServiceDefinition>()
                            ;

                        return (Key: x, Value: wasFound);
                    })
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value);

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<ServiceComponentDataSet, bool>> HasServiceDefinitions(IEnumerable<ServiceComponentDataSet> serviceDefinitionDataSets)
        {
            serviceDefinitionDataSets.VerifyDistinctByNamedFilePathedData();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceDefinitionsHash = new HashSet<INamedFilePathed>(
                    fileContext.ServiceDefinitions,
                    NamedFilePathedEqualityComparer.Instance);

                var output = serviceDefinitionDataSets
                    .Select(x => new { ServiceComponentDataSet = x, Exists = serviceDefinitionsHash.Contains(x) })
                    .ToDictionary(
                        x => x.ServiceComponentDataSet,
                        x => x.Exists);

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<string, ServiceDefinition[]>> HasServiceDefinitionsByCodeFilePath(IEnumerable<string> codeFilePaths)
        {
            var codeFilePathsHash = codeFilePaths.ToHashSet();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var output = fileContext.ServiceDefinitions
                    .Where(x => codeFilePathsHash.Contains(x.CodeFilePath))
                    .Select(x => x.ToAppType())
                    .GroupBy(x => x.CodeFilePath)
                    .ToDictionary(
                        x => x.Key,
                        x => x.ToArray());

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<string, ServiceDefinition[]>> HasServiceDefinitionsByTypeName(IEnumerable<string> typeNames)
        {
            var typeNamesHash = typeNames.ToHashSet();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var output = fileContext.ServiceDefinitions
                    .Where(x => typeNamesHash.Contains(x.TypeName))
                    .Select(x => x.ToAppType())
                    .GroupBy(x => x.TypeName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.ToArray());

                return Task.FromResult(output);
            });

            return output;
        }

        public async Task<Dictionary<ServiceComponentDataSet, bool>> HasServiceImplementations(IEnumerable<ServiceComponentDataSet> serviceImplementationDataSets)
        {
            serviceImplementationDataSets.VerifyDistinctByNamedFilePathedData();

            var output = await this.ExecuteInContext(fileContext =>
            {
                var serviceImplementationsHash = new HashSet<INamedFilePathed>(
                    fileContext.ServiceImplementations,
                    NamedFilePathedEqualityComparer.Instance);

                var output = serviceImplementationDataSets
                    .Select(x => new { ServiceComponentDataSet = x, Exists = serviceImplementationsHash.Contains(x) })
                    .ToDictionary(
                        x => x.ServiceComponentDataSet,
                        x => x.Exists);

                return Task.FromResult(output);
            });

            return output;
        }
    }
}
