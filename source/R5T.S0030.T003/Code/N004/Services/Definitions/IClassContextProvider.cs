using System;

using R5T.T0064;


namespace R5T.S0030.T003.N004
{
    [ServiceDefinitionMarker]
    public interface IClassContextProvider : IServiceDefinition
    {
        N003.IClassContextProvider ClassContextProvider_N003 { get; }
        N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; }
        N002.INamespaceContextProvider NamespaceContextProvider { get; }
    }
}
