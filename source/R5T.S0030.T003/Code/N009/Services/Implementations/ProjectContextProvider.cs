using System;

using R5T.Lombardy;

using R5T.D0079;
using R5T.T0064;


namespace R5T.S0030.T003.N009
{
    [ServiceImplementationMarker]
    public class ProjectContextProvider : IProjectContextProvider, IServiceImplementation
    {
        public IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        public IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }


        public ProjectContextProvider(
            IStringlyTypedPathOperator stringlyTypedPathOperator,
            IVisualStudioProjectFileOperator visualStudioProjectFileOperator)
        {
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
            this.VisualStudioProjectFileOperator = visualStudioProjectFileOperator;
        }
    }
}
