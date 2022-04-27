using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;


namespace R5T.S0030
{
    /// <summary>
    /// Survey all projects in the project repository, find service definitions, then write them out to a file.
    /// </summary>
    [OperationMarker]
    public class O001_IdentifyServiceDefinitionsCore : IOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private IServiceDefinitionCodeFilePathsProvider ServiceDefinitionCodeFilePathsProvider { get; }
        private IServiceDefinitionTypeIdentifier ServiceDefinitionTypeIdentifier { get; }


        public O001_IdentifyServiceDefinitionsCore(
            ILogger<O001_IdentifyServiceDefinitionsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceDefinitionCodeFilePathsProvider serviceDefinitionCodeFilePathsProvider,
            IServiceDefinitionTypeIdentifier serviceDefinitionTypeIdentifier)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceDefinitionCodeFilePathsProvider = serviceDefinitionCodeFilePathsProvider;
            this.ServiceDefinitionTypeIdentifier = serviceDefinitionTypeIdentifier;
        }

        public async Task<List<IServiceDefinitionDescriptor>> Run()
        {
            var serviceDefinitionDescriptors = new List<IServiceDefinitionDescriptor>();

            this.Logger.LogDebug("Identifying service definition types...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();

            //// For debugging.
            //var projectFilePaths = new[]
            //{
            //    @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.E0016.Private\source\R5T.E0016.Lib.EmailRepository\R5T.E0016.Lib.EmailRepository.csproj"
            //};

            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                await Instances.Operation.IdentifyServiceDefinitionsInProject(
                    projectFilePath,
                    this.ServiceDefinitionCodeFilePathsProvider,
                    this.ServiceDefinitionTypeIdentifier,
                    serviceDefinitionDescriptors);
            }

            this.Logger.LogInformation("Identified service definition types.");

            return serviceDefinitionDescriptors;
        }
    }
}
