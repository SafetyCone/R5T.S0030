using System;

using R5T.T0064;


namespace R5T.S0030.T003.N013
{
    [ServiceImplementationMarker]
    public class InterfaceContextProvider : IInterfaceContextProvider, IServiceImplementation
    {
        public N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; set; }
        public N012.IInterfaceContextProvider InterfaceContextProvider_N012 { get; set; }
        public N002.INamespaceContextProvider NamespaceContextProvider { get; set; }


        public InterfaceContextProvider(
            N001.ICompilationUnitContextProvider compilationUnitContextProvider,
            N012.IInterfaceContextProvider interfaceContextProvider,
            N002.INamespaceContextProvider namespaceContextProvider)
        {
            this.CompilationUnitContextProvider = compilationUnitContextProvider;
            this.InterfaceContextProvider_N012 = interfaceContextProvider;
            this.NamespaceContextProvider = namespaceContextProvider;
        }
    }
}
