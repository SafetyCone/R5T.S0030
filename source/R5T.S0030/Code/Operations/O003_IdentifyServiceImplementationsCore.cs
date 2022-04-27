using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O003_IdentifyServiceImplementationsCore : IOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private IServiceImplementationTypeIdentifier ServiceImplementationTypeIdentifier { get; }


        public O003_IdentifyServiceImplementationsCore(
            ILogger<O003_IdentifyServiceImplementationsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            IServiceImplementationTypeIdentifier serviceImplementationTypeIdentifier)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceImplementationTypeIdentifier = serviceImplementationTypeIdentifier;
        }

        public async Task<List<IServiceComponentDescriptor>> Run()
        {
            var serviceImplementationDescriptors = new List<IServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying service implementation types...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                await Instances.Operation.IdentifyServiceImplementationsInProject(
                    projectFilePath,
                    this.ServiceImplementationCodeFilePathsProvider,
                    this.ServiceImplementationTypeIdentifier,
                    serviceImplementationDescriptors);
            }

            this.Logger.LogInformation("Identified service implementation types.");

            return serviceImplementationDescriptors;
        }
    }
}
