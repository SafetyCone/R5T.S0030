using System;

using R5T.T0126;

using R5T.S0030.T003.N001;
using R5T.S0030.T003.N002;


namespace R5T.S0030.T003.N004
{
    public class ClassContext : IClassContext
    {
        public ICompilationUnitContext CompilationUnitContext_N001 { get; set; }
        public INamespaceContext NamespaceContext_N002 { get; set; }
        public N003.IClassContext ClassContext_N003 { get; set; }

        public ClassAnnotation ClassAnnotation => this.ClassContext_N003.ClassAnnotation;

        ClassAnnotation IClassContext.Annotation => this.ClassAnnotation;

        IClassContext IHasClassContext.ClassContext_N004 => this;
    }
}
