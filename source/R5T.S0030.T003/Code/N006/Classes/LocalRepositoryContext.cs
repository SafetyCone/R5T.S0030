using System;

using R5T.D0037;


namespace R5T.S0030.T003.N006
{
    public class LocalRepositoryContext : ILocalRepositoryContext
    {
        public IGitOperator GitOperator { get; set; }

        public string DirectoryPath { get; set; }

        public ILocalRepositoryContext LocalRepositoryContext_N006 => this;
    }
}
