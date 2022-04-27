using System;

using R5T.D0116;
using R5T.T0126;


namespace R5T.S0030.T003.N002
{
    public interface INamespaceContext : IHasNamespaceContext, IHasNamespaceAnnotation
    {
        NamespaceAnnotation Annotation { get; }

        IUsingDirectivesFormatter UsingDirectivesFormatter { get; }
    }
}
