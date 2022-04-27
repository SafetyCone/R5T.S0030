using System;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.D0065;
using R5T.T0064;


namespace R5T.S0030
{
    /// <summary>
    /// Provides the file path given an executable-directory relative path.
    /// </summary>
    [ServiceImplementationMarker]
    public class ExecutableDirectoryFilePathProvider : IExecutableDirectoryFilePathProvider, IServiceImplementation
    {
        private IExecutableDirectoryPathProvider ExecutableDirectoryPathProvider { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public ExecutableDirectoryFilePathProvider(
            IExecutableDirectoryPathProvider executableDirectoryPathProvider,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.ExecutableDirectoryPathProvider = executableDirectoryPathProvider;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public async Task<string> GetFilePath(string executableDirectoryRelativeFilePath)
        {
            var executableDirectoryPath = await this.ExecutableDirectoryPathProvider.GetExecutableDirectoryPath();

            var output = this.StringlyTypedPathOperator.Combine(
                executableDirectoryPath,
                executableDirectoryRelativeFilePath);

            return output;
        }
    }
}
