using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;

using R5T.S0030.Library;


namespace R5T.S0030
{
    /// <summary>
    /// Given a list of distinct project files to search and a list of distinct service definition namespaced type names, runs the full service implementation identification, description, and verification process.
    /// Produces a list of service implementation descriptors.
    /// </summary>
    public class O105A_IdentifyServiceImplementations : IOperation
    {
        private ILogger Logger { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }


        public O105A_IdentifyServiceImplementations(
            ILogger<O105A_IdentifyServiceImplementations> logger,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider)
        {
            this.Logger = logger;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
        }

        public async Task<ServiceImplementationDescriptor[]> Run(
            IDistinctList<string> projectFilePaths,
            IDistinctList<string> serviceDefinitionNamespacedTypeNames,
            string failedTypeCandidatesFilePath,
            string failedCandidatesFilePath,
            string implementationsFilePath)
        {
            this.Logger.LogInformation($"Starting operation {nameof(O105A_IdentifyServiceImplementations)}...");

            var allCandidateTypeFailures = new List<CodeFileTypeUpgradeFailureSet>();
            var allCandidateImplementationFailures = new List<CodeFileCandidateUpgradeFailureSet>();
            var allImplementationAndProjectPairs = new List<ProjectImplementationSuccessSet>();

            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                var serviceImplementationCandidateCodeFilePaths = await this.ServiceImplementationCodeFilePathsProvider.GetServiceImplementationCodeFilePaths(
                    projectFilePath);

                var successes = new List<CodeFileImplementationSuccessSet>();

                foreach (var codeFilePath in serviceImplementationCandidateCodeFilePaths)
                {
                    var compilationUnit = await Instances.CompilationUnitOperator.Load(codeFilePath);

                    // Get candidate classes.
                    var candidateClasses = Instances.Operation.GetCandidateClasses(
                        compilationUnit,
                        out var failedCandidates);

                    if (failedCandidates.Any())
                    {
                        allCandidateTypeFailures.Add(new CodeFileTypeUpgradeFailureSet
                        {
                            CodeFilePath = codeFilePath,
                            UpgradeResults = failedCandidates,
                        });
                    }

                    // Now get implementation types.
                    var upgradeResultPairs = new List<(T001.Level01.UpgradeResult UpgradeResult, T001.Level02.ImplementationDescriptor ImplementationDescriptor)>();

                    foreach (var classDeclaration in candidateClasses)
                    {
                        var implementationDescriptor = Instances.ServiceImplementationOperator.Describe(
                            classDeclaration,
                            compilationUnit,
                            serviceDefinitionNamespacedTypeNames);

                        var upgradeResult = Instances.Operation.Upgrade(
                            implementationDescriptor,
                            out var upgradedImplementationDescriptor);

                        upgradeResultPairs.Add((upgradeResult, upgradedImplementationDescriptor));
                    }

                    var failures = new List<T001.Level01.UpgradeResult>();
                    var implementationSuccesses = new List<ImplementationSuccess>();

                    foreach (var (upgradeResult, implementationDescriptor) in upgradeResultPairs)
                    {
                        var succeeded = upgradeResult.ValidationSuccess.Succeeded();
                        if (succeeded)
                        {
                            implementationSuccesses.Add(new ImplementationSuccess
                            {
                                Implementation = implementationDescriptor,
                                Result = upgradeResult,
                            });
                        }
                        else
                        {
                            failures.Add(upgradeResult);
                        }
                    }

                    if (implementationSuccesses.Any())
                    {
                        successes.Add(new CodeFileImplementationSuccessSet
                        {
                            CodeFilePath = codeFilePath,
                            ImplementationSuccesses = implementationSuccesses.ToArray(),
                        });
                    }

                    if (failures.Any())
                    {
                        allCandidateImplementationFailures.Add(new CodeFileCandidateUpgradeFailureSet
                        {
                            CodeFilePath = codeFilePath,
                            UpgradeResults = failures.ToArray(),
                        });
                    }
                }

                if (successes.Any())
                {
                    allImplementationAndProjectPairs.Add(new ProjectImplementationSuccessSet
                    {
                        ProjectFilePath = projectFilePath,
                        ImplementationSuccesses = successes.ToArray(),
                    });
                }
            }

            // Write outputs.
            this.Logger.LogInformation("Writing output files...");

            var failedTypeCandidateLines = Instances.Operation.DescribeFailedCandidateTypes(
                allCandidateTypeFailures);

            await FileHelper.WriteAllLines(
                failedTypeCandidatesFilePath,
                failedTypeCandidateLines);

            var failedCandidateLines = Instances.Operation.DescribeFailedCandidates(
                allCandidateImplementationFailures);

            await FileHelper.WriteAllLines(
                failedCandidatesFilePath,
                failedCandidateLines);

            var lines = Instances.Operation.DescribeImplementations(
                allImplementationAndProjectPairs);

            await FileHelper.WriteAllLines(
                implementationsFilePath,
                lines);

            // Create output data structures.
            this.Logger.LogInformation("Creating output data instances...");

            var output = allImplementationAndProjectPairs
                .SelectMany(x => x.ImplementationSuccesses
                    .SelectMany(y => y.ImplementationSuccesses
                        .Select(z => new ServiceImplementationDescriptor
                        {
                            NamespacedTypeName = z.Implementation.NamespacedTypeName,

                            HasServiceDefinition = z.Implementation.HasServiceDefinition,
                            ServiceDefinitionNamespacedTypeName = z.Implementation.ServiceDefinitionNamespacedTypeName,

                            HasServiceDependencies = z.Implementation.HasServiceDependencies,
                            ServiceDependencyNamespacedTypeNames = z.Implementation.ServiceDependencyNamespacedTypeNames,

                            CodeFilePath = y.CodeFilePath,
                            ProjectFilePath = x.ProjectFilePath,
                        })))
                .Now();

            this.Logger.LogInformation($"Finished operation {nameof(O105A_IdentifyServiceImplementations)}.");

            return output;
        }
    }
}
