using System;


namespace R5T.S0030.T003.N004
{
    public interface IClassContextProvider
    {
        N003.IClassContextProvider ClassContextProvider_N003 { get; }
        N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; }
        N002.INamespaceContextProvider NamespaceContextProvider { get; }
    }
}
