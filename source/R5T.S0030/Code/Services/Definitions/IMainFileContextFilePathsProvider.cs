using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceDefinitionMarker]
    public interface IMainFileContextFilePathsProvider : IServiceDefinition
    {
        Task<string> GetProjectsJsonFilePath();
        Task<string> GetServiceComponentToProjectMappingsJsonFilePath();
        Task<string> GetServiceDefinitionsJsonFilePath();
        Task<string> GetServiceImplementationsJsonFilePath();
        Task<string> GetToDependencyDefinitionMappingsJsonFilePath();
        Task<string> GetToImplementedDefinitionMappingsJsonFilePath();
    }
}
