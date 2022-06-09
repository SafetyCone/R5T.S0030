using System;

using R5T.Lombardy;

using R5T.D0078;
using R5T.D0083;


namespace R5T.S0030.T003.N008
{
    public class SolutionContext : ISolutionContext
    {
        public IStringlyTypedPathOperator StringlyTypedPathOperator { get; set; }
        public IVisualStudioProjectFileReferencesProvider VisualStudioProjectFileReferencesProvider { get; set; }
        public IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; set; }

        ISolutionContext IHasSolutionContext.SolutionContext => this;

        public string SolutionFilePath { get; set; }
    }
}
