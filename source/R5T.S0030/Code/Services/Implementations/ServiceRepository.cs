using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.T0064;


namespace R5T.S0030.Repositories
{
    [ServiceImplementationMarker]
    public class ServiceRepository<TFileContext> : FileContextBasedRepositoryBase<TFileContext>, IServiceRepository, T0064.IServiceImplementation
        where TFileContext : FileContext, FileContexts.IServiceFileContext
    {
        public ServiceRepository(IFileContextProvider<TFileContext> fileContextProvider)
            : base(fileContextProvider)
        {
        }

        public Task<Dictionary<Guid, ServiceDefinition>> AddServiceDefinitions(IEnumerable<ServiceDefinitionDescriptor> descriptors)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<Guid, bool>> DeleteServiceDefinitions(IEnumerable<Guid> identities)
        {
            throw new NotImplementedException();
        }

        public Task<Guid[]> DeleteServiceDefinitions(Func<IServiceDefinition, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDefinition[]> GetServiceDefinitions(Func<IServiceDefinition, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TValue[]> GetServiceDefinitionValues<TValue>(Func<IServiceDefinition, bool> predicate, Func<IServiceDefinition, TValue> selector)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<Guid, WasFound<ServiceDefinition>>> HasServiceDefinitions(IEnumerable<Guid> identities)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<ServiceDefinitionDescriptor, WasFound<ServiceDefinition>>> HasServiceDefinitions(IEnumerable<ServiceDefinitionDescriptor> descriptors)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, ServiceDefinition[]>> HasServiceDefinitionsByCodeFilePath(IEnumerable<string> codeFilePathss)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, ServiceDefinition[]>> HasServiceDefinitionsByTypeName(IEnumerable<string> typeNames)
        {
            throw new NotImplementedException();
        }
    }
}
