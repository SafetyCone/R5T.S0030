using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O001A_DescribeServiceComponents : IOperation
    {
        private ILogger Logger { get; }


        public O001A_DescribeServiceComponents(
            ILogger<O001A_DescribeServiceComponents> logger)
        {
            this.Logger = logger;
        }

        public async Task Run(
            string outputTextFilePath,
            IEnumerable<IServiceComponentDescriptor> serviceComponentDescriptors)
        {
            this.Logger.LogDebug($"Describing service components to:\n{outputTextFilePath}");

            var orderedServiceDefinitionDescriptors = serviceComponentDescriptors
                .OrderBy(x => x.TypeName)
                ;

            var typesOnlyLines = orderedServiceDefinitionDescriptors
                .Select(x => x.TypeName)
                ;

            var withCodeFileLines = orderedServiceDefinitionDescriptors
                .Select(x => $"{x.TypeName}:\n{x.CodeFilePath}\n")
                .Now();

            var lines = typesOnlyLines
                // Divider that can be searched for.
                .Append("\n***\n")
                .Concat(withCodeFileLines)
                ;

            await FileHelper.WriteAllLines(
                outputTextFilePath,
                lines);

            this.Logger.LogInformation($"Described service components to:\n{outputTextFilePath}");
        }
    }
}
