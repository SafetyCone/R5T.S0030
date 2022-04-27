using System;
using System.Threading.Tasks;

using R5T.T0064;
using R5T.T0128;
using R5T.T0128.D001;


namespace R5T.S0030.FileContexts
{
    [ServiceImplementationMarker]
    [ImplementsServiceDefinition(typeof(IFileContextProvider<MainFileContext>))]
    public class MainFileContextProvider : IFileContextProvider<MainFileContext>, IServiceImplementation
    {
        private IMainFileContextFilePathsProvider MainFileContextFilePathsProvider { get; }


        public MainFileContextProvider(
            IMainFileContextFilePathsProvider mainFileContextFilePathsProvider)
        {
            this.MainFileContextFilePathsProvider = mainFileContextFilePathsProvider;
        }

        public async Task<MainFileContext> GetFileContext()
        {
            var projectsJsonFilePath = await this.MainFileContextFilePathsProvider.GetProjectsJsonFilePath();
            var serviceComponentToProjectMappingsJsonFilePath = await this.MainFileContextFilePathsProvider.GetServiceComponentToProjectMappingsJsonFilePath();
            var serviceDefinitionsJsonFilePath = await this.MainFileContextFilePathsProvider.GetServiceDefinitionsJsonFilePath();
            var serviceImplementationsJsonFilePath = await this.MainFileContextFilePathsProvider.GetServiceImplementationsJsonFilePath();
            var toDependencyDefinitionMappingsJsonFilePath = await this.MainFileContextFilePathsProvider.GetToDependencyDefinitionMappingsJsonFilePath();
            var toImplementedDefinitionMappingsJsonFilePath = await this.MainFileContextFilePathsProvider.GetToImplementedDefinitionMappingsJsonFilePath();

            var output = new MainFileContext
            {
                Projects = new JsonFileSet<D0101.I001.Entities.Project>(projectsJsonFilePath),
                ServiceComponentToProjectMappings = new JsonFileSet<Entities.ServiceComponentToProjectMapping>(serviceComponentToProjectMappingsJsonFilePath),
                ServiceDefinitions = new JsonFileSet<Entities.ServiceDefinition>(serviceDefinitionsJsonFilePath),
                ServiceImplementations = new JsonFileSet<Entities.ServiceImplementation>(serviceImplementationsJsonFilePath),
                ToDependencyDefinitionMappings = new JsonFileSet<Entities.ImplementionToDefinitionMapping>(toDependencyDefinitionMappingsJsonFilePath),
                ToImplementedDefinitionMappings = new JsonFileSet<Entities.ImplementionToDefinitionMapping>(toImplementedDefinitionMappingsJsonFilePath),
            };

            return output;
        }
    }
}
