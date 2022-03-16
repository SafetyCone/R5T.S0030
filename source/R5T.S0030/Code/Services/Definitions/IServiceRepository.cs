using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.T0064;


namespace R5T.S0030.Repositories
{
    [ServiceDefinitionMarker]
    public interface IServiceRepository : T0064.IServiceDefinition
    {
        #region Service Definition

        Task<Dictionary<Guid, ServiceDefinition>> AddServiceDefinitions(
            IEnumerable<ServiceDefinitionDescriptor> descriptors);

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
            IEnumerable<string> codeFilePathss);

        // Descriptor (type name + code file path) is unique.
        Task<Dictionary<ServiceDefinitionDescriptor, WasFound<ServiceDefinition>>> HasServiceDefinitions(
            IEnumerable<ServiceDefinitionDescriptor> descriptors);

        Task<ServiceDefinition[]> GetServiceDefinitions(Func<IServiceDefinition, bool> predicate);

        Task<TValue[]> GetServiceDefinitionValues<TValue>(Func<IServiceDefinition, bool> predicate, Func<IServiceDefinition, TValue> selector);

        // No update, delete and re-add.
        //Task UpdateServiceDefinition(Guid identity, ServiceDefinitionDescriptor descriptor);

        #endregion
    }
}
