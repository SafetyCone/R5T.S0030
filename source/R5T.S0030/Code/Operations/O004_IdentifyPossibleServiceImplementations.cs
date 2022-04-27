using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O004_IdentifyPossibleServiceImplementations : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceComponents { get; }
        private O002A_DescribePossibleServiceComponents O002A_DescribePossibleServiceComponents { get; }
        private O004_IdentifyPossibleServiceImplementationsCore O004_IdentifyPossibleServiceImplementationsCore { get; }


        public O004_IdentifyPossibleServiceImplementations(
            ILogger<O002_IdentifyPossibleServiceDefinitions> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            O001A_DescribeServiceComponents o001A_DescribeServiceComponents,
            O002A_DescribePossibleServiceComponents o002A_DescribePossibleServiceComponents,
            O004_IdentifyPossibleServiceImplementationsCore o004_IdentifyPossibleServiceImplementationsCore)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.O001A_DescribeServiceComponents = o001A_DescribeServiceComponents;
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

            await this.O001A_DescribeServiceComponents.Run(
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

            /// Create data file of missing-marker-reasoned service definitions for <see cref="O200_AddServiceDefinitionMarkerAttributeAndInterface"/>.
            var missingMarkerServiceDefinitions = reasonedPossibleServiceDefinitionDescriptors
                .Where(x => x.Reason == Reasons.LacksMarkerAttribute)
                .Now();

            var missingMarkerDataFilePath = @"C:\Temp\Missing marker attribute.json";

            JsonFileHelper.WriteToFile(
                missingMarkerDataFilePath,
                missingMarkerServiceDefinitions);

            // Open in Notepad++.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputTextFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(byReasonOutputTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O004_IdentifyPossibleServiceImplementations)}");
        }
    }
}
