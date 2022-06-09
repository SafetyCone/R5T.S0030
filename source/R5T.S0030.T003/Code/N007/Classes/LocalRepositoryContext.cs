using System;


namespace R5T.S0030.T003.N007
{
    public class LocalRepositoryContext : ILocalRepositoryContext
    {
        public N006.ILocalRepositoryContext LocalRepositoryContext_N006 { get; set; }
        public N005.IRemoteRepositoryContext RemoteRepositoryContext_N005 { get; set; }

        public ILocalRepositoryContext LocalRepositoryContext_N007 => this;
    }
}
