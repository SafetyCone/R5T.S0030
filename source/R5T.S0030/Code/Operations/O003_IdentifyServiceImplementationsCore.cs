using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
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

        public async Task<List<ServiceComponentDescriptor>> Run()
        {
            var serviceImplementationDescriptors = new List<ServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying service implementation types...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                var serviceImplementationCodeFilePaths = await this.ServiceImplementationCodeFilePathsProvider.GetServiceImplementationCodeFilePaths(
                    projectFilePath);

                foreach (var serviceDefinitionCodeFilePath in serviceImplementationCodeFilePaths)
                {
                    var typeNamedCodeFilePaths = await this.ServiceImplementationTypeIdentifier.GetServiceImplementationTypes(
                        serviceDefinitionCodeFilePath);

                    var currentServiceImplementationDescriptors = typeNamedCodeFilePaths
                        .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                        .Now();

                    serviceImplementationDescriptors.AddRange(currentServiceImplementationDescriptors);
                }
            }

            this.Logger.LogInformation("Identified service implementation types.");

            return serviceImplementationDescriptors;
        }
    }
}
