using System;

using R5T.D0037;
using R5T.T0064;


namespace R5T.S0030.T003.N006
{
    [ServiceImplementationMarker]
    public class LocalRepositoryContextProvider : ILocalRepositoryContextProvider, IServiceImplementation
    {
        public IGitOperator GitOperator { get; }


        public LocalRepositoryContextProvider(
            IGitOperator gitOperator)
        {
            this.GitOperator = gitOperator;
        }
    }
}
