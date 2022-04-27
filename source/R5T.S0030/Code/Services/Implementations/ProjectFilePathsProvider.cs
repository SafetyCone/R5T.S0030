using System;
using System.Threading.Tasks;

using R5T.D0101;
using R5T.T0064;


namespace R5T.S0030
{
    /// <summary>
    /// Gets all project file paths from the project repository. That way all projects exist in the project repository for mapping.
    /// </summary>
    [ServiceImplementationMarker]
    [ImplementsServiceDefinition(typeof(IProjectFilePathsProvider))]
    public class ProjectFilePathsProvider : IProjectFilePathsProvider, IServiceImplementation
    {
        private IProjectRepository ProjectRepository { get; }


        public ProjectFilePathsProvider(
            IProjectRepository projectRepository)
        {
            this.ProjectRepository = projectRepository;
        }

        public async Task<string[]> GetProjectFilePaths()
        {
            var output = await this.ProjectRepository.GetAllProjectFilePaths();
            return output;
        }
    }
}
