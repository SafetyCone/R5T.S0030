using System;

using R5T.D0082;
using R5T.T0064;


namespace R5T.S0030.T003.N005
{
    [ServiceImplementationMarker]
    public class RemoteRepositoryContextProvider : IRemoteRepositoryContextProvider, IServiceImplementation
    {
        public IGitHubOperator GitHubOperator { get; }


        public RemoteRepositoryContextProvider(
            IGitHubOperator gitHubOperator)
        {
            this.GitHubOperator = gitHubOperator;
        }
    }
}
