using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace R5T.S0030.FileContexts
{
    public class MainFileContext : FileContext,
        IServiceFileContext
    {
        private IMainFileContextFilePathsProvider MainFileContextFilePathsProvider { get; }

        public FileSet<Entities.ServiceDefinition> ServiceDefinitions { get; private set; }


        public MainFileContext(IMainFileContextFilePathsProvider mainFileContextFilePathsProvider)
        {
            this.MainFileContextFilePathsProvider = mainFileContextFilePathsProvider;
        }

        protected override async Task Configure()
        {
            var serviceDefinitionsJsonFilePath = await this.MainFileContextFilePathsProvider.GetServiceDefinitionsJsonFilePath();

            this.ServiceDefinitions = new JsonFileSet<Entities.ServiceDefinition>(serviceDefinitionsJsonFilePath);
        }

        protected override IEnumerable<FileSet> GetAllFileSets()
        {
            return EnumerableHelper.From(
                this.ServiceDefinitions);
        }
    }
}
