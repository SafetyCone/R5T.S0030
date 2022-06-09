using System;

using R5T.Lombardy;

using R5T.D0079;
using R5T.T0064;


namespace R5T.S0030.T003.N009
{
    [ServiceDefinitionMarker]
    public interface IProjectContextProvider : IServiceDefinition
    {
        IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }
    }
}
