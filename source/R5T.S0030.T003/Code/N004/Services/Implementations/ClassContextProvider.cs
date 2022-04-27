using System;


namespace R5T.S0030.T003.N004
{
    public class ClassContextProvider : IClassContextProvider
    {
        public N003.IClassContextProvider ClassContextProvider_N003 { get; }
        public N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; set; }
        public N002.INamespaceContextProvider NamespaceContextProvider { get; set; }


        public ClassContextProvider(
            N003.IClassContextProvider classContextProvider,
            N001.ICompilationUnitContextProvider compilationUnitContextProvider,
            N002.INamespaceContextProvider namespaceContextProvider)
        {
            this.ClassContextProvider_N003 = classContextProvider;
            this.CompilationUnitContextProvider = compilationUnitContextProvider;
            this.NamespaceContextProvider = namespaceContextProvider;
        }
    }
}
