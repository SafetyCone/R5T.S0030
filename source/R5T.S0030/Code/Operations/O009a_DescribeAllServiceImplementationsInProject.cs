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
    public class O009a_DescribeAllServiceImplementationsInProject : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public O009a_DescribeAllServiceImplementationsInProject(
            ILogger<O010_DescribeAllServiceImplementations> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            Repositories.IServiceRepository serviceRepository)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

            /// Run.
            // Get all service definitions.
            var allDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var definitionNamespacedTypeNames = allDefinitions
                .Select(x => x.TypeName)
                .Now();

            this.Logger.LogInformation($"Evaluating project:\n{projectFilePath}");

            // Describe project.
            var result = await Instances.Operation.DescribeServiceImplementationsInProject(
                projectFilePath,
                this.ServiceImplementationCodeFilePathsProvider,
                this.ServiceRepository);


            /// Write outputs.
            var failedTypeCandidatesFilePath = @"C:\Temp\Implementation Candidate Type Failures.txt";

            var failedTypeCandidateLines = Instances.Operation.DescribeFailedCandidateTypes(
                result.CandidateTypeFailures);

            await FileHelper.WriteAllLines(
                failedTypeCandidatesFilePath,
                failedTypeCandidateLines);

            var failedCandidatesFilePath = @"C:\Temp\Implementation Candidate Failures.txt";

            var failedCandidateLines = Instances.Operation.DescribeFailedCandidates(
                result.UpgradeFailures);

            await FileHelper.WriteAllLines(
                failedCandidatesFilePath,
                failedCandidateLines);

            var outputFilePath = @"C:\Temp\Implementation Descriptions.txt";

            var lines = Instances.Operation.DescribeImplementations(
                ArrayHelper.From(
                    result.ProjectImplementationSuccessSet));

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
