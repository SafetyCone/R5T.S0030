using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;

using R5T.S0030.Library;


namespace R5T.S0030
{
    /// <summary>
    /// Initial attempt.
    /// See <see cref="O105A_IdentifyServiceImplementations"/> for an integrated operation.
    /// </summary>
    public class O010_DescribeAllServiceImplementations : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public O010_DescribeAllServiceImplementations(
            ILogger<O010_DescribeAllServiceImplementations> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            Repositories.IServiceRepository serviceRepository)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            var allCandidateTypeFailures = new List<CodeFileTypeUpgradeFailureSet>();
            var allCandidateImplementationFailures = new List<CodeFileCandidateUpgradeFailureSet>();
            var allImplementationAndProjectPairs = new List<ProjectImplementationSuccessSet>();

            // Get all service definitions.
            var allDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var definitionNamespacedTypeNames = allDefinitions
                .Select(x => x.TypeName)
                .Now();

            // Get all project paths.
            var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();

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
                            definitionNamespacedTypeNames);

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
            var failedTypeCandidatesFilePath = @"C:\Temp\Implementation Candidate Type Failures.txt";

            var failedTypeCandidateLines = Instances.Operation.DescribeFailedCandidateTypes(
                allCandidateTypeFailures);

            await FileHelper.WriteAllLines(
                failedTypeCandidatesFilePath,
                failedTypeCandidateLines);

            var failedCandidatesFilePath = @"C:\Temp\Implementation Candidate Failures.txt";

            var failedCandidateLines = Instances.Operation.DescribeFailedCandidates(
                allCandidateImplementationFailures);

            await FileHelper.WriteAllLines(
                failedCandidatesFilePath,
                failedCandidateLines);

            var outputFilePath = @"C:\Temp\Implementation Descriptions.txt";

            var lines = Instances.Operation.DescribeImplementations(
                allImplementationAndProjectPairs);

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);

            // Show output.
            await this.NotepadPlusPlusOperator.OpenFilePath(failedTypeCandidatesFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(failedCandidatesFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }
    }
}
