using System;

using R5T.T0064;


namespace R5T.S0030.T003.N013
{
    [ServiceDefinitionMarker]
    public interface IInterfaceContextProvider : IServiceDefinition
    {
        N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; }
        N012.IInterfaceContextProvider InterfaceContextProvider_N012 { get; }
        N002.INamespaceContextProvider NamespaceContextProvider { get; }
    }
}
