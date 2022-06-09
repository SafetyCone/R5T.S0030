using System;

using R5T.Lombardy;

using R5T.D0078;
using R5T.D0083;


namespace R5T.S0030.T003.N008
{
    public interface ISolutionContext : IHasSolutionContext
    {
        IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        IVisualStudioProjectFileReferencesProvider VisualStudioProjectFileReferencesProvider { get; }
        IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; }

        string SolutionFilePath { get; set; }
    }
}
