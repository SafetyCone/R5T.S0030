using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.Magyar.IO;

using R5T.T0020;


namespace R5T.S0030
{
    [OperationMarker]
    public class O002A_DescribePossibleServiceComponents : IOperation
    {
        private ILogger Logger { get; }


        public O002A_DescribePossibleServiceComponents(
            ILogger<O002A_DescribePossibleServiceComponents> logger)
        {
            this.Logger = logger;
        }

        public async Task Run(
            string outputTextFilePath,
            IEnumerable<IReasonedServiceComponentDescriptor> reasonedServiceComponentDescriptors,
            IComparer<string> reasonOutputSortOrderComparer)
        {
            this.Logger.LogDebug($"Describing possible service components to:\n{outputTextFilePath}");

            var byReasonLines = reasonedServiceComponentDescriptors
                .GroupBy(x => x.Reason)
                .OrderBy(
                    x => x.Key,
                    reasonOutputSortOrderComparer)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(y => y.TypeName).ToArray())
                .GetLines();

            await FileHelper.WriteAllLines(
                outputTextFilePath,
                byReasonLines);

            this.Logger.LogInformation($"Described possible service components to:\n{outputTextFilePath}");
        }
    }
}
