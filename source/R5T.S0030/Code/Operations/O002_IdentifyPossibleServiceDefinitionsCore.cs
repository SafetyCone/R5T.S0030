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
    public class O002_IdentifyPossibleServiceDefinitionsCore : IOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }


        public O002_IdentifyPossibleServiceDefinitionsCore(
            ILogger<O002_IdentifyPossibleServiceDefinitionsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
        }

        public async Task<List<IReasonedServiceComponentDescriptor>> Run()
        {
            var possibleServiceDefinitionDescriptors = new List<IReasonedServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying possible service definitions...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogDebug($"Evaluating project:\n{projectFilePath}");

                await Instances.Operation.IdentifyPossibleServiceDefinitionsInProject(
                    projectFilePath,
                    this.Logger,
                    possibleServiceDefinitionDescriptors);

                this.Logger.LogInformation($"Evaluated project:\n{projectFilePath}");
            }

            this.Logger.LogInformation("Identified service definitions.");

            return possibleServiceDefinitionDescriptors;
        }
    }
}
