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

            var projectServiceImplementationsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(
                projectDirectoryPath);

            var serviceImplementationDirectoryDescendentFilePaths = Instances.FileSystemOperator.GetAllDescendentFilePathsOrEmptyIfNotExists(projectServiceImplementationsDirectoryPath);

            return Task.FromResult(serviceImplementationDirectoryDescendentFilePaths);
        }
    }
}
