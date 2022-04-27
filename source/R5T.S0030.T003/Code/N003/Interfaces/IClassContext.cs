using System;

using R5T.T0126;


namespace R5T.S0030.T003.N003
{
    public interface IClassContext : IHasClassAnnotation, IHasClassContext
    {
        ClassAnnotation Annotation { get; }
    }
}
