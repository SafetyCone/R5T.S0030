using R5T.D0082;
using System;


namespace R5T.S0030.T003.N005
{
    public class RemoteRepositoryContext : IRemoteRepositoryContext
    {
        public IGitHubOperator GitHubOperator { get; set; }

        public string Name { get; set; }

        public IRemoteRepositoryContext RemoteRepositoryContext_N005 => this;
    }
}
