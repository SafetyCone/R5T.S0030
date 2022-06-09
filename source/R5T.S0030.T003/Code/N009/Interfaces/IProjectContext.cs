using System;

using R5T.Lombardy;

using R5T.D0079;


namespace R5T.S0030.T003.N009
{
    public interface IProjectContext : IHasProjectContext
    {
        IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }

        string ProjectFilePath { get; }
    }
}
