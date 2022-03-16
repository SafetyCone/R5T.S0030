﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    /// <summary>
    /// Identifies service definitions that have the required service definition marker attribute, but lack the suggested service definition marker interface.
    /// </summary>
    [OperationMarker]
    public class O006_IdentityServiceImplementationsLackingMarkerInterface : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceComponents { get; }


        public O006_IdentityServiceImplementationsLackingMarkerInterface(
            ILogger<O006_IdentityServiceImplementationsLackingMarkerInterface> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectFilePathsProvider projectFilePathsProvider,
            O001A_DescribeServiceComponents o001A_DescribeServiceDefinitions)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.O001A_DescribeServiceComponents = o001A_DescribeServiceDefinitions;
        }

        public async Task Run()
        {
            /// Inputs.
            var outputTextFilePath = @"C:\Temp\Service Implementations-Missing marker interface.txt";

            /// Run.
            var descriptorsOfImproperServiceDefinitions = new List<IServiceComponentDescriptor>();

            this.Logger.LogDebug("Identifying service implementations with marker attribute, but without marker interface...");

            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogDebug($"Evaluating project:\n{projectFilePath}");

                var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

                var servicesImplementationsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(projectDirectoryPath);

                var directoryExists = Instances.FileSystemOperator.DirectoryExists(servicesImplementationsDirectoryPath);
                if (directoryExists)
                {
                    var servicesImplementationsCodeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(servicesImplementationsDirectoryPath);

                    foreach (var servicesImplementationsCodeFilePath in servicesImplementationsCodeFilePaths)
                    {
                        var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                            servicesImplementationsCodeFilePath,
                            compilationUnit =>
                            {
                                var classes = compilationUnit.GetClasses();

                                var classesOfInterest = classes
                                    .Where(x => true
                                        && Instances.ClassOperator.HasServiceImplementationMarkerAttribute(x)
                                        && Instances.ClassOperator.LacksServiceDefinitionMarkerInterface(x))
                                    .Now();

                                var classTypeNames = classesOfInterest.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                                return Task.FromResult(classTypeNames);
                            });

                        var serviceComponentDescriptors = typeNamedCodeFilePaths
                            .Select(x => x.GetReasonedServiceComponentDescriptor(
                                projectFilePath,
                                Reasons.LacksMarkerInterface))
                            .Now();

                        descriptorsOfImproperServiceDefinitions.AddRange(serviceComponentDescriptors);
                    }
                }
            }

            this.Logger.LogInformation("Identified service implementations with marker attribute, but without marker interface.");

            await this.O001A_DescribeServiceComponents.Run(
                outputTextFilePath,
                descriptorsOfImproperServiceDefinitions);

            await this.NotepadPlusPlusOperator.OpenFilePath(outputTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O002_IdentifyPossibleServiceDefinitions)}");
        }
    }
}