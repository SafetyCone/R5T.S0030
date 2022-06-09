using System;

using R5T.D0037;


namespace R5T.S0030.T003.N006
{
    public interface ILocalRepositoryContext : IHasLocalRepositoryContext
    {
        IGitOperator GitOperator { get; }

        string DirectoryPath { get; }
    }
}
