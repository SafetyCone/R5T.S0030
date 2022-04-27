using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O105_AddServiceImplementationsToRepository : IActionOperation
    {
        private ILogger Logger { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }

        private O105A_IdentifyServiceImplementations O105A_IdentifyServiceImplementations { get; }
        private O105B_AddServiceImplementationsToRepository O105B_AddServiceImplementationsToRepository { get; }


        public O105_AddServiceImplementationsToRepository(
            ILogger<O105_AddServiceImplementationsToRepository> logger,
            IProjectFilePathsProvider projectFilePathsProvider,
            Repositories.IServiceRepository serviceRepository,

            O105A_IdentifyServiceImplementations o105A_IdentifyServiceImplementations,
            O105B_AddServiceImplementationsToRepository o105B_AddServiceImplementationsToRepository)
        {
            this.Logger = logger;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceRepository = serviceRepository;

            this.O105A_IdentifyServiceImplementations = o105A_IdentifyServiceImplementations;
            this.O105B_AddServiceImplementationsToRepository = o105B_AddServiceImplementationsToRepository;
        }

        public async Task Run()
        {
            /// Inputs.
            var failedTypeCandidatesFilePath = @"C:\Temp\Implementation Candidate Type Failures.txt";
            var failedCandidatesFilePath = @"C:\Temp\Implementation Candidate Failures.txt";
            var implementationsFilePath = @"C:\Temp\Implementation Descriptions.txt";

            /// Run.
            this.Logger.LogInformation($"Starting operation {nameof(O105_AddServiceImplementationsToRepository)}...");

            // Project file path.s
            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();

            var duplicateProjectFilePaths = projectFilePaths.GetDuplicatesInAlphabeticalOrder().Now();
            if(duplicateProjectFilePaths.Any())
            {
                throw new Exception("Duplicate project file paths found.");
            }

            var distinctProjectFilePaths = projectFilePaths.AsDistinctList();

            // Service definitions.
            // TODO: there should be a unique service definition selection process, but fake it for now.
            var serviceDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var serviceDefinitionNamespacedTypeNames = serviceDefinitions.GetAllNames()
                .GetDistinct()
                .Now();

            var duplicateServiceDefinitionNamespacedTypeNames = serviceDefinitionNamespacedTypeNames.GetDuplicatesInAlphabeticalOrder().Now();
            if(duplicateServiceDefinitionNamespacedTypeNames.Any())
            {
                throw new Exception("Duplicate service definition namespaced type names found.");
            }

            var distinctServiceDefinitionNamespacedTypeNames = serviceDefinitionNamespacedTypeNames.AsDistinctList();

            // Run sub-operations.
            var serviceImplementationDescriptors = await this.O105A_IdentifyServiceImplementations.Run(
                distinctProjectFilePaths,
                distinctServiceDefinitionNamespacedTypeNames,
                failedTypeCandidatesFilePath,
                failedCandidatesFilePath,
                implementationsFilePath);

            //// For debugging.
            //var dataCacheJsonFilePath = @"C:\Temp\Service implementation descriptors.json";

            //JsonFileHelper.WriteToFile(
            //    dataCacheJsonFilePath,
            //    serviceImplementationDescriptors);

            //var serviceImplementationDescriptors = JsonFileHelper.LoadFromFile<ServiceImplementationDescriptor[]>(
            //    dataCacheJsonFilePath);

            await this.O105B_AddServiceImplementationsToRepository.Run(
                serviceImplementationDescriptors);

            this.Logger.LogInformation($"Finished operation {nameof(O105_AddServiceImplementationsToRepository)}.");
        }
    }
}
