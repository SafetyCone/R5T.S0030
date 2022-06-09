using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar.Results;

using R5T.D0105;
using R5T.T0020;

using R5T.S0030.Library;


namespace R5T.S0030
{
    /// <summary>
    /// Given a code file path and the namespaced type name of a type in the code file, describe, validate, and upgrade the type to a service implementation.
    /// </summary>
    public class O008_DescribeSingleServiceImplementation : IActionOperation
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public O008_DescribeSingleServiceImplementation(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            Repositories.IServiceRepository serviceRepository)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            /// Inputs.
            var codeFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\Code\Services\Implementations\MainFileContextProvider.cs";
            var implementationNamespacedTypeName = @"R5T.S0030.FileContexts.MainFileContextProvider";

            /// Run.
            var compilationUnit = await Instances.CompilationUnitOperator_Old.Load(codeFilePath);
            var classDeclaration = compilationUnit.GetClassByNamespacedTypeName(implementationNamespacedTypeName);

            var allDefinitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var definitionNamespacedTypeNames = allDefinitions
                .Select(x => x.TypeName)
                .Now();

            var implementationDescriptor = Instances.ServiceImplementationOperator.Describe(
                classDeclaration,
                compilationUnit,
                definitionNamespacedTypeNames);

            var upgradeResult = Instances.Operation.Upgrade(
                implementationDescriptor,
                out var upgradedImplementationDescriptor);

            // Write output.
            var outputFilePath = @"C:\Temp\Implementation Description.txt";

            var lines = Instances.Operation.DescribeImplementation(
                upgradeResult,
                upgradedImplementationDescriptor,
                codeFilePath);

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);

            // Show output.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }
    }
}
