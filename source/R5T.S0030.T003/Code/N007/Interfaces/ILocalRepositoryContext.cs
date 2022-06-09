using System;


namespace R5T.S0030.T003.N007
{
    public interface ILocalRepositoryContext : IHasLocalRepositoryContext,
        N006.IHasLocalRepositoryContext,
        N005.IHasRemoteRepositoryContext
    {
    }
}
