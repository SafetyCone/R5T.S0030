using System;

using R5T.D0116;
using R5T.T0064;


namespace R5T.S0030.T003.N001
{
    [ServiceImplementationMarker]
    public class CompilationUnitContextProvider : ICompilationUnitContextProvider, IServiceImplementation
    {
        public IUsingDirectivesFormatter UsingDirectivesFormatter { get; }


        public CompilationUnitContextProvider(
            IUsingDirectivesFormatter usingDirectivesFormatter)
        {
            this.UsingDirectivesFormatter = usingDirectivesFormatter;
        }
    }
}
