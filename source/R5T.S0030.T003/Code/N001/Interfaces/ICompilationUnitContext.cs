using System;

using R5T.D0116;


namespace R5T.S0030.T003.N001
{
    public interface ICompilationUnitContext : IHasCompilationUnitContext
    {
        IUsingDirectivesFormatter UsingDirectivesFormatter { get; }
    }
}
