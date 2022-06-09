using System;

using R5T.D0084.D002;
using R5T.T0064;


namespace R5T.S0030.T003.N007
{
    [ServiceImplementationMarker]
    public class LocalRepositoryContextProvider : ILocalRepositoryContextProvider, IServiceImplementation
    {
        public N006.ILocalRepositoryContextProvider LocalRepositoryContextProvider_N006 { get; }
        public N005.IRemoteRepositoryContextProvider RemoteRepositoryContextProvider_N005 { get; }
        public IRepositoriesDirectoryPathProvider RepositoriesDirectoryPathProvider { get; }


        public LocalRepositoryContextProvider(
            N006.ILocalRepositoryContextProvider localRepositoryContextProvider,
            N005.IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            IRepositoriesDirectoryPathProvider repositoriesDirectoryPathProvider)
        {
            this.LocalRepositoryContextProvider_N006 = localRepositoryContextProvider;
            this.RemoteRepositoryContextProvider_N005 = remoteRepositoryContextProvider;
            this.RepositoriesDirectoryPathProvider = repositoriesDirectoryPathProvider;
        }
    }
}
