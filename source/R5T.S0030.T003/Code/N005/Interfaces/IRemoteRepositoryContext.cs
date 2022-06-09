using System;

using R5T.D0082;


namespace R5T.S0030.T003.N005
{
    public interface IRemoteRepositoryContext : IHasRemoteRepositoryContext
    {
        IGitHubOperator GitHubOperator { get; }

        string Name { get; }
    }
}
