using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0030
{
    /// <summary>
    /// Sorts the service component files containing namespaced type names alphabetically.
    /// </summary>
    public class O900_SortExternalServiceComponentDataFiles : IActionOperation
    {
        public async Task Run()
        {
            var filePaths = new[]
            {
                @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\Files\ServiceDefinitions-External.txt",
                @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\Files\ServiceImplementations-External.txt",
            };

            foreach (var filePath in filePaths)
            {
                var inputLines = await FileHelper.ReadAllLines(filePath);

                var outputLines = inputLines.OrderAlphabetically().Now();

                await FileHelper.WriteAllLines(
                    filePath,
                    outputLines);
            }
        }
    }
}
