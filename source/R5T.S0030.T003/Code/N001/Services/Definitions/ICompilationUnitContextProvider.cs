using System;

using R5T.D0116;


namespace R5T.S0030.T003.N001
{
    public interface ICompilationUnitContextProvider
    {
        IUsingDirectivesFormatter UsingDirectivesFormatter { get; }
    }
}
