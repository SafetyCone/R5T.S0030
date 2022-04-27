using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0105;
using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O003_IdentifyServiceImplementations : IActionOperation
    {
        private ILogger Logger { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private O001A_DescribeServiceComponents O001A_DescribeServiceComponents { get; }
        private O003_IdentifyServiceImplementationsCore O003_IdentifyServiceImplementationsCore { get; }


        public O003_IdentifyServiceImplementations(
            ILogger<O003_IdentifyServiceImplementations> logger,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            O001A_DescribeServiceComponents o001A_DescribeServiceComponents,
            O003_IdentifyServiceImplementationsCore o003_IdentifyServiceImplementationsCore)
        {
            this.Logger = logger;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.O001A_DescribeServiceComponents = o001A_DescribeServiceComponents;
            this.O003_IdentifyServiceImplementationsCore = o003_IdentifyServiceImplementationsCore;
        }

        public async Task Run()
        {
            /// Inputs.
            var outputTextFilePath = @"C:\Temp\Service Implementations-Found.txt";

            /// Run.
            var serviceImplementationDescriptors = await this.O003_IdentifyServiceImplementationsCore.Run();

            this.Logger.LogDebug($"Writing service implementations to:\n{outputTextFilePath}");

            await this.O001A_DescribeServiceComponents.Run(
                outputTextFilePath,
                serviceImplementationDescriptors);

            this.Logger.LogInformation($"Wrote service implementations to:\n{outputTextFilePath}");

            // Open in Notepad++.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputTextFilePath);

            this.Logger.LogInformation($"Finished operation {typeof(O003_IdentifyServiceImplementations)}");
        }
    }
}
