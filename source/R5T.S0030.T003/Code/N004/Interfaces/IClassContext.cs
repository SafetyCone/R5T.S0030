using System;

using R5T.T0126;


namespace R5T.S0030.T003.N004
{
    public interface IClassContext :
        IHasClassContext,
        IHasClassAnnotation,
        N001.IHasCompilationUnitContext,
        N002.IHasNamespaceContext,
        N003.IHasClassContext
    {
        ClassAnnotation Annotation { get; }
    }
}
