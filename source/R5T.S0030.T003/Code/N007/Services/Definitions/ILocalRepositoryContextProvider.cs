using System;

using R5T.D0084.D002;
using R5T.T0064;


namespace R5T.S0030.T003.N007
{
    [ServiceDefinitionMarker]
    public interface ILocalRepositoryContextProvider : IServiceDefinition
    {
        N005.IRemoteRepositoryContextProvider RemoteRepositoryContextProvider_N005 { get; }
        N006.ILocalRepositoryContextProvider LocalRepositoryContextProvider_N006 { get; }

        // GitOperator, provider by N006.ILocalRepositoryContextProvider.
        IRepositoriesDirectoryPathProvider RepositoriesDirectoryPathProvider { get; }
        // RepositoryFileSystemConventions? Currently using T0108.IRepositoryNameOperator base.
        // StringlyTypedPathsOperator? Currently using IPathOperator base.
    }
}
