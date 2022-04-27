using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.D0105;
using R5T.T0020;

using R5T.S0030.T001.Level01;
using R5T.S0030.T002.ImplementationCandidates.Level01;

using R5T.S0030.Library;

using Level02 = R5T.S0030.T001.Level02;


namespace R5T.S0030
{
    /// <summary>
    /// Given a code file path, describe, validate, and upgrade all service implementations in the code file, starting by selecting candidate types.
    /// </summary>
    public class O009_DescribeServiceImplementationsInFile : IActionOperation
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public O009_DescribeServiceImplementationsInFile(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            Repositories.IServiceRepository serviceRepository)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            /// Inputs.
            var codeFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\Code\Services\Implementations\ServiceRepository.cs";

            /// Run.
            var compilationUnit = await Instances.CompilationUnitOperator.Load(codeFilePath);

            // Get candidate classes.
            var candidateClasses = Instances.Operation.GetCandidateClasses(
                compilationUnit,
                out var failedCandidates);

            // Now analyze candidate classes.
            var allDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var definitionNamespacedTypeNames = allDefinitions
                .Select(x => x.TypeName)
                .Now();

            var upgradeResultPairs = new List<(UpgradeResult UpgradeResult, Level02.ImplementationDescriptor ImplementationDescriptor)>();

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

            // Write outputs.
            var candidateFailuresFilePath = @"C:\Temp\Implementation Candidate Type Failures.txt";

            var lines2 = Instances.Operation.DescribeFailedCandidateTypes(
                failedCandidates,
                codeFilePath);

            await FileHelper.WriteAllLines(
                candidateFailuresFilePath,
                lines2);

            var outputFilePath = @"C:\Temp\Implementation Description.txt";

            var lines = Instances.Operation.DescribeImplementations(
                upgradeResultPairs,
                codeFilePath);

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);

            // Show output.
            await this.NotepadPlusPlusOperator.OpenFilePath(candidateFailuresFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }
    }
}
