using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    /// <summary>
    /// Survey all projects in the project repository, find service definitions, then write them out to a file.
    /// </summary>
    [OperationMarker]
    public class O001_IdentifyServiceDefinitions : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceDefinitions { get; }
        private O001_IdentifyServiceDefinitionsCore O001_IdentityServiceDefinitionsCore { get; }


        public O001_IdentifyServiceDefinitions(
            ILogger<O001_IdentifyServiceDefinitions> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            O001A_DescribeServiceComponents o001A_DescribeServiceDefinitions,
            O001_IdentifyServiceDefinitionsCore o001_IdentityServiceDefinitionsCore)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.O001A_DescribeServiceDefinitions = o001A_DescribeServiceDefinitions;
            this.O001_IdentityServiceDefinitionsCore = o001_IdentityServiceDefinitionsCore;
        }

        public async Task Run()
        {
            /// Inputs.
            var outputTextFilePath = @"C:\Temp\Service Definitions-Found.txt";

            /// Run.
            var serviceDefinitionDescriptors = await this.O001_IdentityServiceDefinitionsCore.Run();

            this.Logger.LogDebug($"Writing service definitions to:\n{outputTextFilePath}");

            await this.O001A_DescribeServiceDefinitions.Run(
                outputTextFilePath,
                serviceDefinitionDescriptors);

            this.Logger.LogInformation($"Wrote service definitions to:\n{outputTextFilePath}");

            // Open in Notepad++.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O001_IdentifyServiceDefinitions)}");
        }
    }
}
