using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.T0064;
using R5T.T0097;


namespace R5T.S0030.Repositories
{
    [ServiceDefinitionMarker]
    public interface IServiceRepository : T0064.IServiceDefinition
    {
        #region Service Definitions

        Task<Dictionary<Guid, ServiceDefinition>> AddServiceDefinitions(
            IEnumerable<ServiceComponentDataSet> dataSets);

        Task<Dictionary<Guid, bool>> DeleteServiceDefinitions(
            IEnumerable<Guid> identities);

        Task<Guid[]> DeleteServiceDefinitions(Func<IServiceDefinition, bool> predicate);

        //Task<Dictionary<TKey, ServiceDefinition[]>> HasServiceDefinitions<TKey>(
        //    IEnumerable<TKey> keys,
        //    Func<IServiceDefinition, TKey> keySelector);

        Task<Dictionary<Guid, WasFound<ServiceDefinition>>> HasServiceDefinitions(
            IEnumerable<Guid> identities);
        
        // Type name is not unique.
        Task<Dictionary<string, ServiceDefinition[]>> HasServiceDefinitionsByTypeName(
            IEnumerable<string> typeNames);

        // Code file path is not unique.
        Task<Dictionary<string, ServiceDefinition[]>> HasServiceDefinitionsByCodeFilePath(
            IEnumerable<string> codeFilePaths);

        // Descriptor (type name + code file path) is unique.
        Task<Dictionary<ServiceComponentDataSet, bool>> HasServiceDefinitions(
            IEnumerable<ServiceComponentDataSet> serviceDefinitionDataSets);

        Task<Dictionary<ITypeNamedCodeFilePathed, WasFound<ServiceDefinition>>> HasServiceDefinitions(
            IEnumerable<ITypeNamedCodeFilePathed> typeNamedCodeFilePatheds);

        Task<ServiceDefinition[]> GetServiceDefinitions(Func<IServiceDefinition, bool> predicate);

        Task<TValue[]> GetServiceDefinitionValues<TValue>(Func<IServiceDefinition, bool> predicate, Func<IServiceDefinition, TValue> selector);

        // No update, delete and re-add.
        //Task UpdateServiceDefinition(Guid identity, ServiceDefinitionDescriptor descriptor);

        #endregion

        #region Service Definition-to-Project Mappings

        Task AddProjectsForServiceDefinitions(
            IEnumerable<(ServiceDefinition serviceDefinition, Project project)> instancePairs);

        Task<Dictionary<ServiceDefinition, bool>> DeleteProjectsForServiceDefinitions(
            IEnumerable<(ServiceDefinition serviceDefinition, Project project)> instancePairs);

        Task<(ServiceDefinition serviceDefinition, Project project)[]> GetAllServiceDefinitionToProjectMappings();

        Task<Dictionary<ServiceDefinition, WasFound<Project>>> HasProjectsForServiceDefinitions(
            IEnumerable<ServiceDefinition> serviceDefinitions);

        //Task SetProjectsForServiceDefinitions(
        //    IEnumerable<(Project project, ServiceDefinition serviceDefinition)> instancePairs);

        //Task SetProjectsForServiceDefinitions(
        //    IEnumerable<(Guid projectIdentity, Guid serviceDefinitionIdentity)> identityPairs);

        #endregion

        #region Service Implementations

        Task<Dictionary<Guid, ServiceImplementation>> AddServiceImplementations(
            IEnumerable<ServiceComponentDataSet> dataSets);

        Task<Dictionary<Guid, bool>> DeleteServiceImplementations(
            IEnumerable<Guid> identities);

        Task<ServiceImplementation[]> GetServiceImplementations(Func<IServiceImplementation, bool> predicate);

        Task<Dictionary<ServiceComponentDataSet, bool>> HasServiceImplementations(
            IEnumerable<ServiceComponentDataSet> serviceImplementationDataSets);

        #endregion

        #region Service Implementation-to-Project Mappings

        Task AddProjectsForServiceImplementations(
            IEnumerable<(ServiceImplementation serviceImplementation, Project project)> instancePairs);

        Task<Dictionary<ServiceImplementation, bool>> DeleteProjectsForServiceImplementations(
            IEnumerable<(ServiceImplementation serviceImplementation, Project project)> instancePairs);

        Task<(ServiceImplementation serviceImplementation, Project project)[]> GetAllServiceImplementationToProjectMappings();

        Task<Dictionary<ServiceImplementation, WasFound<Project>>> HasProjectsForServiceImplementations(
            IEnumerable<ServiceImplementation> serviceImplementations);

        //Task SetProjectsForServiceDefinitions(
        //    IEnumerable<(Project project, ServiceDefinition serviceDefinition)> instancePairs);

        //Task SetProjectsForServiceDefinitions(
        //    IEnumerable<(Guid projectIdentity, Guid serviceDefinitionIdentity)> identityPairs);

        #endregion

        #region Service Implementation-to-Implemented Service Definition Mappings

        Task AddImplementedDefinitions(
            IEnumerable<ImplementedDefinitionMapping> mappings);

        Task<Dictionary<ImplementedDefinitionMapping, bool>> DeleteImplementedDefinitions(
            IEnumerable<ImplementedDefinitionMapping> mappings);

        Task<ImplementedDefinitionMapping[]> GetAllImplementedDefinitionMappings();

        Task<Dictionary<IServiceImplementationDataIdentity, WasFound<ServiceDefinition>>> HasImplementedDefinitions(
            IEnumerable<IServiceImplementationDataIdentity> implementationDataIdentities);

        //Task<WasFound<ImplementedDefinitionMapping>[]> HasImplementedDefinitionMappings(
        //    IEnumerable<ImplementedDefinitionMapping> mappings);

        #endregion

        #region Implementation-to-Dependency Definitions Mappings

        Task AddDependencyDefinitions(
            IEnumerable<DependencyDefinitionMapping> mappings);

        Task<Dictionary<DependencyDefinitionMapping, bool>> DeleteDependencyDefinitions(
            IEnumerable<DependencyDefinitionMapping> mappings);

        Task<DependencyDefinitionMapping[]> GetAllDependencyDefinitionMappings();

        Task<Dictionary<IServiceImplementationDataIdentity, WasFound<ServiceDefinition[]>>> HasDependencyDefinitions(
            IEnumerable<IServiceImplementationDataIdentity> implementationDataIdentities);

        #endregion
    }
}
