using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0096;
using R5T.T0020;


namespace R5T.S0030
{
    public class O999_Scratch : IActionOperation
    {
#pragma warning disable IDE0052 // Remove unread private members
        private IHumanOutput HumanOutput { get; }
#pragma warning restore IDE0052 // Remove unread private members
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private IServiceDefinitionCodeFilePathsProvider ServiceDefinitionCodeFilePathsProvider { get; }
        private IServiceDefinitionTypeIdentifier ServiceDefinitionDescriptorProvider { get; }


        public O999_Scratch(
            IHumanOutput humanOutput,
            ILogger<O999_Scratch> logger,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceDefinitionCodeFilePathsProvider serviceDefinitionCodeFilePathsProvider,
            IServiceDefinitionTypeIdentifier serviceDefinitionDescriptorProvider)
        {
            this.HumanOutput = humanOutput;
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceDefinitionCodeFilePathsProvider = serviceDefinitionCodeFilePathsProvider;
            this.ServiceDefinitionDescriptorProvider = serviceDefinitionDescriptorProvider;
        }

        public async Task Run()
        {
            var serviceDefinitionDescriptors = new List<ServiceDefinitionDescriptor>();

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
                        .Select(x => x.GetServiceDefinitionDescriptor(projectFilePath))
                        .Now();

                    serviceDefinitionDescriptors.AddRange(currentServiceDefinitionDescriptors);
                }
            }
        }
    }
}
