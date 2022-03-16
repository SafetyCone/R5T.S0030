using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceImplementationMarker]
    public class ServiceImplementationCodeFilePathsProvider : IServiceImplementationCodeFilePathsProvider, IServiceImplementation
    {
        public Task<string[]> GetServiceImplementationCodeFilePaths(string projectFilePath)
        {
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(
                projectFilePath);

            var projectServiceDefinitionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(
                projectDirectoryPath);

            var serviceDefinitionDirectoryDescendentFilePaths = Instances.FileSystemOperator.GetAllDescendentFilePathsOrEmptyIfNotExists(projectServiceDefinitionsDirectoryPath);

            return Task.FromResult(serviceDefinitionDirectoryDescendentFilePaths);
        }
    }
}
