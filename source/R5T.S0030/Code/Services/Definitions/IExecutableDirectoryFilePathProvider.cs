using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    /// <summary>
    /// Provides the file path given an executable-directory relative path.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface IExecutableDirectoryFilePathProvider : IServiceDefinition
    {
        Task<string> GetFilePath(string executableDirectoryRelativeFilePath);
    }
}
