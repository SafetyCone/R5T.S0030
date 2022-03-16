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
        private IServiceDefinitionTypeIdentifier ServiceDefinitionDescriptorProvider { get; }


        public O001_IdentifyServiceDefinitionsCore(
            ILogger<O001_IdentifyServiceDefinitionsCore> logger,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceDefinitionCodeFilePathsProvider serviceDefinitionCodeFilePathsProvider,
            IServiceDefinitionTypeIdentifier serviceDefinitionDescriptorProvider)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceDefinitionCodeFilePathsProvider = serviceDefinitionCodeFilePathsProvider;
            this.ServiceDefinitionDescriptorProvider = serviceDefinitionDescriptorProvider;
        }

        public async Task<List<ServiceComponentDescriptor>> Run()
        {
            var serviceDefinitionDescriptors = new List<ServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying service definition types...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                var serviceDefinitionCodeFilePaths = await this.ServiceDefinitionCodeFilePathsProvider.GetServiceDefinitionCodeFilePaths(
                    projectFilePath);

                foreach (var serviceDefinitionCodeFilePath in serviceDefinitionCodeFilePaths)
                {
                    var typeNamedCodeFilePaths = await this.ServiceDefinitionDescriptorProvider.GetServiceDefinitionTypes(
                        serviceDefinitionCodeFilePath);

                    var currentServiceDefinitionDescriptors = typeNamedCodeFilePaths
                        .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                        .Now();

                    serviceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors);
                }
            }

            this.Logger.LogInformation("Identified service definition types.");

            return serviceDefinitionDescriptors;
        }
    }
}
