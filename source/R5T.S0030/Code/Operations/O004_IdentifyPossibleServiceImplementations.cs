using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O004_IdentifyPossibleServiceImplementations : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceDefinitions { get; }
        private O002A_DescribePossibleServiceComponents O002A_DescribePossibleServiceComponents { get; }
        private O004_IdentifyPossibleServiceImplementationsCore O004_IdentifyPossibleServiceImplementationsCore { get; }


        public O004_IdentifyPossibleServiceImplementations(
            ILogger<O002_IdentifyPossibleServiceDefinitions> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            O001A_DescribeServiceComponents o001A_DescribeServiceDefinitions,
            O002A_DescribePossibleServiceComponents o002A_DescribePossibleServiceComponents,
            O004_IdentifyPossibleServiceImplementationsCore o004_IdentifyPossibleServiceImplementationsCore)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.O001A_DescribeServiceDefinitions = o001A_DescribeServiceDefinitions;
            this.O002A_DescribePossibleServiceComponents = o002A_DescribePossibleServiceComponents;
            this.O004_IdentifyPossibleServiceImplementationsCore = o004_IdentifyPossibleServiceImplementationsCore;
        }

        public async Task Run()
        {
            /// Inputs.
            var outputTextFilePath = @"C:\Temp\Service Implementations-Possible.txt";
            var byReasonOutputTextFilePath = @"C:\Temp\Service Implementations-Possible, by Reason.txt";

            /// Run.
            var reasonedPossibleServiceDefinitionDescriptors = await this.O004_IdentifyPossibleServiceImplementationsCore.Run();

            this.Logger.LogDebug($"Writing possible service implementations to:\n{outputTextFilePath}");

            await this.O001A_DescribeServiceDefinitions.Run(
                outputTextFilePath,
                reasonedPossibleServiceDefinitionDescriptors);

            this.Logger.LogInformation($"Wrote possible service implementations to:\n{outputTextFilePath}");

            var reasonOutputSortOrderComparer = new SpecifiedListComparer<string>(
                        Reasons.LacksMarkerAttribute,
                        Reasons.InOldServicesClassesDirectory);

            await this.O002A_DescribePossibleServiceComponents.Run(
                byReasonOutputTextFilePath,
                reasonedPossibleServiceDefinitionDescriptors,
                reasonOutputSortOrderComparer);

            // Open in Notepad++.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(byReasonOutputTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O004_IdentifyPossibleServiceImplementations)}");
        }
    }
}
