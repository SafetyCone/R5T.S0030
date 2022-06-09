using System;

using R5T.Lombardy;

using R5T.D0078;
using R5T.D0083;
using R5T.T0064;


namespace R5T.S0030.T003.N008
{
    [ServiceImplementationMarker]
    public class SolutionContextProvider : ISolutionContextProvider, IServiceImplementation
    {
        public IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        public IVisualStudioProjectFileReferencesProvider VisualStudioProjectFileReferencesProvider { get; }
        public IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; }


        public SolutionContextProvider(
            IStringlyTypedPathOperator stringlyTypedPathOperator,
            IVisualStudioProjectFileReferencesProvider visualStudioProjectFileReferencesProvider,
            IVisualStudioSolutionFileOperator visualStudioSolutionFileOperator)
        {
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
            this.VisualStudioProjectFileReferencesProvider = visualStudioProjectFileReferencesProvider;
            this.VisualStudioSolutionFileOperator = visualStudioSolutionFileOperator;
        }
    }
}
