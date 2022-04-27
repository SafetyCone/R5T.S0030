using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0094;
using R5T.T0097;

using R5T.S0030.Repositories;

using Instances = R5T.S0030.Instances;


namespace System
{
    public static class IServiceRepositoryExtensions
    {
        public static async Task<Dictionary<ServiceDefinition, bool>> DeleteServiceDefinitions(this IServiceRepository serviceRepository,
            IEnumerable<ServiceDefinition> serviceDefinitions)
        {
            // Ensure all service definitions have identitie, and distinct identities in preparation for creating a dictionary keyed by identity.
            serviceDefinitions.VerifyAllIdentitiesAreSet();
            serviceDefinitions.VerifyDistinctByIdentity();

            var serviceDefinitionsByIdentity = serviceDefinitions.ToDictionaryByIdentity();

            var serviceDefinitionIdentities = serviceDefinitionsByIdentity.Keys;

            var wasDeletedByIdentity = await serviceRepository.DeleteServiceDefinitions(serviceDefinitionIdentities);

            var output = serviceDefinitionsByIdentity
                .Join(wasDeletedByIdentity,
                    x => x.Key,
                    x => x.Key,
                    (serviceDefinitionByIdentityPair, wasDeletedByIdentityPair) => new { ServiceDefinition = serviceDefinitionByIdentityPair.Value, WasDeleted = wasDeletedByIdentityPair.Value })
                .ToDictionary(
                    x => x.ServiceDefinition,
                    x => x.WasDeleted,
                    NamedIdentifiedFilePathedEqualityComparer<ServiceDefinition>.Instance);

            return output;
        }

        public static async Task<Dictionary<ServiceImplementation, bool>> DeleteServiceImplementations(this IServiceRepository serviceRepository,
            IEnumerable<ServiceImplementation> serviceImplementations)
        {
            // Ensure all service definitions have identitie, and distinct identities in preparation for creating a dictionary keyed by identity.
            serviceImplementations.VerifyAllIdentitiesAreSet();
            serviceImplementations.VerifyDistinctByIdentity();

            var serviceImplementationsByIdentity = serviceImplementations.ToDictionaryByIdentity();

            var serviceImplementationIdentities = serviceImplementationsByIdentity.Keys;

            var wasDeletedByIdentity = await serviceRepository.DeleteServiceImplementations(serviceImplementationIdentities);

            var output = serviceImplementationsByIdentity
                .Join(wasDeletedByIdentity,
                    x => x.Key,
                    x => x.Key,
                    (serviceImplementationByIdentityPair, wasDeletedByIdentityPair) => new { ServiceImplementation = serviceImplementationByIdentityPair.Value, WasDeleted = wasDeletedByIdentityPair.Value })
                .ToDictionary(
                    x => x.ServiceImplementation,
                    x => x.WasDeleted,
                    NamedIdentifiedFilePathedEqualityComparer<ServiceImplementation>.Instance);

            return output;
        }

        public static async Task<ServiceDefinition[]> GetAllServiceDefinitions(this IServiceRepository serviceRepository)
        {
            var output = await serviceRepository.GetServiceDefinitions(
                Instances.Predicate.True<IServiceDefinition>());

            return output;
        }

        public static async Task<ServiceImplementation[]> GetAllServiceImplementations(this IServiceRepository serviceRepository)
        {
            var output = await serviceRepository.GetServiceImplementations(
                Instances.Predicate.True<IServiceImplementation>());

            return output;
        }

        //public static async Task<Dictionary<ServiceDefinition, Project>> GetProjectsForServiceDefinitions(this IServiceRepository serviceRepository,
        //    IEnumerable<ServiceDefinition> serviceDefinitions)
        //{
        //    var hasProjects = await serviceRepository.HasProjectsForServiceDefinitions(serviceDefinitions);

        //    var anyServiceDefinitionsWithoutProject = hasProjects.AnyNotFound();
        //    if(anyServiceDefinitionsWithoutProject)
        //    {
        //        throw new Exception("Some service definitions had no project.");
        //    }

        //    var output = hasProjects.ToDictionaryFromWasFounds();
        //    return output;
        //}
    }
}
