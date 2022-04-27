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
    /// <summary>
    /// Survey all projects in the project repository, find service definitions, then write them out to a file.
    /// </summary>
    [OperationMarker]
    public class O002_IdentifyPossibleServiceDefinitions : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceComponents { get; }
        private O002A_DescribePossibleServiceComponents O002A_DescribePossibleServiceComponents { get; }
        private O002_IdentifyPossibleServiceDefinitionsCore O002_IdentifyPossibleServiceDefinitionsCore { get; }


        public O002_IdentifyPossibleServiceDefinitions(
            ILogger<O002_IdentifyPossibleServiceDefinitions> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            O001A_DescribeServiceComponents o001A_DescribeServiceDefinitions,
            O002A_DescribePossibleServiceComponents o002A_DescribePossibleServiceComponents,
            O002_IdentifyPossibleServiceDefinitionsCore o002_IdentifyPossibleServiceDefinitionsCore)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.O001A_DescribeServiceComponents = o001A_DescribeServiceDefinitions;
            this.O002A_DescribePossibleServiceComponents = o002A_DescribePossibleServiceComponents;
            this.O002_IdentifyPossibleServiceDefinitionsCore = o002_IdentifyPossibleServiceDefinitionsCore;
        }

        public async Task Run()
        {
            /// Inputs.
            var outputTextFilePath = @"C:\Temp\Service Definitions-Possible.txt";
            var byReasonOutputTextFilePath = @"C:\Temp\Service Definitions-Possible, by Reason.txt";

            /// Run.
            var reasonedPossibleServiceDefinitionDescriptors = await this.O002_IdentifyPossibleServiceDefinitionsCore.Run();

            this.Logger.LogDebug($"Writing possible service definitions to:\n{outputTextFilePath}");

            await this.O001A_DescribeServiceComponents.Run(
                outputTextFilePath,
                reasonedPossibleServiceDefinitionDescriptors);

            this.Logger.LogInformation($"Wrote possible service definitions to:\n{outputTextFilePath}");

            var reasonOutputSortOrderComparer = new SpecifiedListComparer<string>(
                        Reasons.LacksMarkerAttribute,
                        Reasons.InOldServicesInterfacesDirectory);

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
            await this.NotepadPlusPlusOperator.OpenFilePath(missingMarkerDataFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O002_IdentifyPossibleServiceDefinitions)}");
        }
    }
}
