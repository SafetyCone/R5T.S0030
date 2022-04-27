using System;

using R5T.D0116;


namespace R5T.S0030.T003.N001
{
    public class CompilationUnitContext : ICompilationUnitContext
    {
        public IUsingDirectivesFormatter UsingDirectivesFormatter { get; set; }

        ICompilationUnitContext IHasCompilationUnitContext.CompilationUnitContext_N001 => this;
    }
}
