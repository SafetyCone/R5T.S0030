using System;

using R5T.D0037;
using R5T.T0064;


namespace R5T.S0030.T003.N006
{
    [ServiceDefinitionMarker]
    public interface ILocalRepositoryContextProvider : IServiceDefinition
    {
        IGitOperator GitOperator { get; }

        // FileSystemOperator service? For now using R5T.T0044.IFileSystemOperator base.
    }
}
