using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceImplementationMarker]
    public class ServiceDefinitionCodeFilePathsProvider : IServiceDefinitionCodeFilePathsProvider, T0064.IServiceImplementation
    {
        public Task<string[]> GetServiceDefinitionCodeFilePaths(string projectFilePath)
        {
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(
                projectFilePath);

            var projectServiceDefinitionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesDefinitionsDirectoryPath(
                projectDirectoryPath);

            var serviceDefinitionDirectoryDescendentFilePaths = Instances.FileSystemOperator.GetAllDescendentFilePathsOrEmptyIfNotExists(projectServiceDefinitionsDirectoryPath);
            
            return Task.FromResult(serviceDefinitionDirectoryDescendentFilePaths);
        }
    }
}
