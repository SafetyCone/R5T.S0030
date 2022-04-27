using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using R5T.Magyar.Results;

using R5T.D0105;
using R5T.T0020;
using R5T.T0094;

using R5T.S0030.Repositories;


namespace R5T.S0030
{
    /// <summary>
    /// This operation works on classes in code files in the \Code\Services\Implementations directory of projects.
    /// It collects all service implementation candidates (class in the \Code\Services\Implementation directories of projects), and then passes them through the 
    /// It processes each candidate builds up a list of what service definitions satisfy what
    /// </summary>
    public class O007_IdentifyServiceImplementations : IActionOperation
    {
        #region Static

        private static void AddCandidateResults(
            ITypeNamedCodeFilePathed candidateTypeNamedCodeFilePathed,
            MultipleActionResult multipleActionResult,
            Dictionary<ITypeNamedCodeFilePathed, List<string>> resultsByCandidateNamespacedTypeName)
        {
            foreach (var actionResult in multipleActionResult.ActionResults)
            {
                if (actionResult.Result.Unsuccessful())
                {
                    resultsByCandidateNamespacedTypeName.AddValueByKey(candidateTypeNamedCodeFilePathed, actionResult.Message);
                }
            }
        }

        private static async Task WriteFailures(
            string outputFilePath,
            Dictionary<ITypeNamedCodeFilePathed, List<string>> failuresByCandidateNamespacedTypeName)
        {
            var lines = EnumerableHelper.From(
                $"Count: {failuresByCandidateNamespacedTypeName.Count}\n\n")
                .AppendRange(
                    failuresByCandidateNamespacedTypeName
                    // Order alphabetically by service implementation namespaced type name.
                    .OrderAlphabetically(x => x.Key.TypeName)
                    .SelectMany(x => EnumerableHelper.From(
                        $"{Instances.NamespacedTypeNameOperator.GetTypeName(x.Key.TypeName)}: {x.Key.TypeName}")
                        .AppendRange(x.Value
                            .Select(y => $"\t{y}"))
                        .Append($"\n\t{x.Key.CodeFilePath}")
                        .Append(String.Empty)))
                .Now();

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);
        }

        #endregion


        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectFilePathsProvider ProjectFilePathsProvider { get; }
        private IServiceImplementationCandidateIdentifier ServiceImplementationCandidateIdentifier { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private IServiceRepository ServiceRepository { get; }


        public O007_IdentifyServiceImplementations(
            ILogger<O007_IdentifyServiceImplementations> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectFilePathsProvider projectFilePathsProvider,
            IServiceImplementationCandidateIdentifier serviceImplementationCandidateIdentifier,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            IServiceRepository serviceRepository)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectFilePathsProvider = projectFilePathsProvider;
            this.ServiceImplementationCandidateIdentifier = serviceImplementationCandidateIdentifier;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            this.Logger.LogInformation($"Starting operation {typeof(O007_IdentifyServiceImplementations)}...");

            var failuresByCandidateNamespacedTypeName = new Dictionary<ITypeNamedCodeFilePathed, List<string>>(
                NamedFilePathedEqualityComparer<ITypeNamedCodeFilePathed>.Instance);

            var successfulCandidateNamespacedTypeNames = new List<ITypeNamedCodeFilePathed>();

            {
                this.Logger.LogInformation("Querying for all available service definitions...");

                var definitions = await this.ServiceRepository.GetAllServiceDefinitions();

                var availableDefinitionNamespacedTypeNames = definitions
                    .Select(x => x.TypeName)
                    .Now();

                this.Logger.LogInformation("Identifying service implementation types...");

                var projectFilePaths = await this.ProjectFilePathsProvider.GetProjectFilePaths();

                //// For debugging.
                //var projectFilePaths = new[]
                //{
                //    @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj",
                //};

                foreach (var projectFilePath in projectFilePaths)
                {
                    this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

                    // Discover service implementation candidates in project.
                    var candidates = new List<ServiceImplementationCandidate>();

                    var codeFilePaths = await this.ServiceImplementationCodeFilePathsProvider.GetServiceImplementationCodeFilePaths(projectFilePath);
                    foreach (var codeFilePath in codeFilePaths)
                    {
                        var currentCandidates = await this.ServiceImplementationCandidateIdentifier.GetCandidateServiceImplementations(codeFilePath);

                        candidates.AddRange(currentCandidates);
                    }

                    // Run service implementation candidates through the levels.
                    foreach (var candidate in candidates)
                    {
                        var candidateTypeNamedCodeFilePathed = new TypeNamedCodeFilePathed
                        {
                            TypeName = candidate.Class.GetNamespacedTypeName(),
                            CodeFilePath = candidate.CodeFilePath,
                        };

                        //// For debugging.
                        //if (candidateTypeNamedCodeFilePathed.TypeName != "R5T.S0030.Repositories.ServiceRepository")
                        //{
                        //    continue;
                        //}

                        this.Logger.LogInformation($"Evaluating candidate: {candidateTypeNamedCodeFilePathed.TypeName}");

                        var meetsLevel00 = Instances.ServiceImplementationOperator.SatisfiesRequirements_Level_00(
                            candidate.Class);

                        if (!meetsLevel00.Succeeded())
                        {
                            O007_IdentifyServiceImplementations.AddCandidateResults(
                                candidateTypeNamedCodeFilePathed,
                                meetsLevel00,
                                failuresByCandidateNamespacedTypeName);

                            continue;
                        }

                        var meetsLevel01 = Instances.ServiceImplementationOperator.SatisfiesRequirements_Level_01(
                            candidate.Class);

                        if (!meetsLevel01.Succeeded())
                        {
                            O007_IdentifyServiceImplementations.AddCandidateResults(
                                candidateTypeNamedCodeFilePathed,
                                meetsLevel01,
                                failuresByCandidateNamespacedTypeName);

                            continue;
                        }

                        var meetsLevel02 = Instances.ServiceImplementationOperator.SatisfiesRequirements_Level_02(
                            candidate.Class,
                            candidate.CompilationUnit,
                            availableDefinitionNamespacedTypeNames);

                        if (!meetsLevel02.Succeeded())
                        {
                            O007_IdentifyServiceImplementations.AddCandidateResults(
                                candidateTypeNamedCodeFilePathed,
                                meetsLevel02,
                                failuresByCandidateNamespacedTypeName);

                            continue;
                        }

                        var meetsLevel03 = Instances.ServiceImplementationOperator.SatisfiesRequirements_Level_03(
                            candidate.Class,
                            candidate.CompilationUnit,
                            availableDefinitionNamespacedTypeNames);

                        if (!meetsLevel03.Succeeded())
                        {
                            O007_IdentifyServiceImplementations.AddCandidateResults(
                                candidateTypeNamedCodeFilePathed,
                                meetsLevel03,
                                failuresByCandidateNamespacedTypeName);

                            continue;
                        }

                        var meetsLevel04 = Instances.ServiceImplementationOperator.SatisfiesRequirements_Level_04(
                            candidate.Class);

                        if (!meetsLevel04.Succeeded())
                        {
                            O007_IdentifyServiceImplementations.AddCandidateResults(
                                candidateTypeNamedCodeFilePathed,
                                meetsLevel04,
                                failuresByCandidateNamespacedTypeName);

                            continue;
                        }

                        // Great! Another success.
                        successfulCandidateNamespacedTypeNames.Add(candidateTypeNamedCodeFilePathed);
                    }
                }

                this.Logger.LogInformation("Identified service implementation types.");
            }

            // Cache results for work on display.
            var failureDataFilePath = @"C:\Temp\Failures.json";
            var successesDataFilePath = @"C:\Temp\Successes.json";

            JsonFileHelper.WriteToFile(
                failureDataFilePath,
                failuresByCandidateNamespacedTypeName);

            JsonFileHelper.WriteToFile(
                successesDataFilePath,
                successfulCandidateNamespacedTypeNames);

            //var failuresByCandidateNamespacedTypeName = JsonFileHelper.LoadFromFile<Dictionary<string, List<string>>>(
            //    failureDataFilePath);

            //var successfulCandidateNamespacedTypeNames = JsonFileHelper.LoadFromFile<List<string>>(
            //    successesDataFilePath);


            // Write output.
            var allFailuresOutputFilePath = @"C:\Temp\Candidate Service Implementation-All Failures.txt";

            await O007_IdentifyServiceImplementations.WriteFailures(
                allFailuresOutputFilePath,
                failuresByCandidateNamespacedTypeName);

            // Actionable failures.
            var failuresOutputFilePath = @"C:\Temp\Candidate Service Implementation-Failures.txt";

            var actionableFailuresByCandidateNamespacedTypeName = failuresByCandidateNamespacedTypeName
                .Where(x => x.Value
                    .Where(x => x != "Service implementation must not be abstract." && x != "Service implementation must not be static.")
                    .Any())
                .ToDictionary(
                    x => x.Key,
                    x => x.Value);

            await O007_IdentifyServiceImplementations.WriteFailures(
                failuresOutputFilePath,
                actionableFailuresByCandidateNamespacedTypeName);

            var successesOutputFilePath = @"C:\Temp\Candidate Service Implementation-Successes.txt";

            var lines = EnumerableHelper.From(
                $"Count: {successfulCandidateNamespacedTypeNames.Count}\n\n")
                .AppendRange(
                    successfulCandidateNamespacedTypeNames
                        .OrderAlphabetically(x => x.TypeName)
                        .Select(x => x.TypeName))
                .Now();

            await FileHelper.WriteAllLines(
                successesOutputFilePath,
                lines);

            // Show in Notepad++.
            await this.NotepadPlusPlusOperator.OpenFilePath(successesOutputFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(allFailuresOutputFilePath);
            // Open actionable failures last to be helpful.
            await this.NotepadPlusPlusOperator.OpenFilePath(failuresOutputFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O007_IdentifyServiceImplementations)}.");
        }
    }
}
