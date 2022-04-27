using System;

using R5T.D0116;


namespace R5T.S0030.T003.N001
{
    public class CompilationUnitContextProvider : ICompilationUnitContextProvider
    {
        public IUsingDirectivesFormatter UsingDirectivesFormatter { get; }


        public CompilationUnitContextProvider(
            IUsingDirectivesFormatter usingDirectivesFormatter)
        {
            this.UsingDirectivesFormatter = usingDirectivesFormatter;
        }
    }
}
