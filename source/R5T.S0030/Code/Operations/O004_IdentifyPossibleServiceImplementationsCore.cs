using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O004_IdentifyPossibleServiceImplementationsCore : IOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }


        public O004_IdentifyPossibleServiceImplementationsCore(
            ILogger<O004_IdentifyPossibleServiceImplementationsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
        }

        public async Task<List<IReasonedServiceComponentDescriptor>> Run()
        {
            var possibleServiceImplementationDescriptors = new List<IReasonedServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying possible service implementations...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogDebug($"Evaluating project:\n{projectFilePath}");

                await Instances.Operation.IdentifyPossibleServiceImplementationsInProject(
                    projectFilePath,
                    this.Logger,
                    possibleServiceImplementationDescriptors);

                this.Logger.LogInformation($"Evaluated project:\n{projectFilePath}");
            }

            this.Logger.LogInformation("Identified service implementations.");

            return possibleServiceImplementationDescriptors;
        }
    }
}
