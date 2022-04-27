using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceImplementationMarker]
    public class HardCodedMainFileContextFilePathsProvider : IMainFileContextFilePathsProvider, IServiceImplementation
    {
        public Task<string> GetProjectsJsonFilePath()
        {
            var output = @"C:\Temp\Projects-All.json";

            return Task.FromResult(output);
        }

        public Task<string> GetServiceDefinitionsJsonFilePath()
        {
            var output = @"C:\Temp\Service Definitions.json";

            return Task.FromResult(output);
        }

        public Task<string> GetServiceComponentToProjectMappingsJsonFilePath()
        {
            var output = @"C:\Temp\Service Components-To Project Mappings.json";

            return Task.FromResult(output);
        }

        public Task<string> GetServiceImplementationsJsonFilePath()
        {
            var output = @"C:\Temp\Service Implementations.json";

            return Task.FromResult(output);
        }

        public Task<string> GetToImplementedDefinitionMappingsJsonFilePath()
        {
            var output = @"C:\Temp\To Implemented Definition Mappings.json";

            return Task.FromResult(output);
        }

        public Task<string> GetToDependencyDefinitionMappingsJsonFilePath()
        {
            var output = @"C:\Temp\To Dependency Definition Mappings.json";

            return Task.FromResult(output);
        }
    }
}
