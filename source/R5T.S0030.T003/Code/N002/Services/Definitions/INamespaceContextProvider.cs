using System;

using R5T.D0116;
using R5T.T0064;


namespace R5T.S0030.T003.N002
{
    [ServiceDefinitionMarker]
    public interface INamespaceContextProvider : IServiceDefinition
    {
        IUsingDirectivesFormatter UsingDirectivesFormatter { get; }
    }
}
