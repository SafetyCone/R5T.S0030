using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceImplementationMarker]
    public class ServiceDefinitionCodeFilePathsProvider : IServiceDefinitionCodeFilePathsProvider, IServiceImplementation
    {
        public Task<string[]> GetServiceDefinitionCodeFilePaths(string projectFilePath)
        {
            return Instances.ProjectPathsOperator.GetServicesDefinitionsDirectoryFilePaths(projectFilePath);
        }
    }
}
