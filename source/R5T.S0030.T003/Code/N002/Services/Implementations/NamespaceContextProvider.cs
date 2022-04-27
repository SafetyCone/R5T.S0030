using System;

using R5T.D0116;


namespace R5T.S0030.T003.N002
{
    public class NamespaceContextProvider : INamespaceContextProvider
    {
        public IUsingDirectivesFormatter UsingDirectivesFormatter { get; }


        public NamespaceContextProvider(
            IUsingDirectivesFormatter usingDirectivesFormatter)
        {
            this.UsingDirectivesFormatter = usingDirectivesFormatter;
        }
    }
}
