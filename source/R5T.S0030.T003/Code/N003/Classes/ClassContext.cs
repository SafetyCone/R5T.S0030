using System;

using R5T.T0126;


namespace R5T.S0030.T003.N003
{
    public class ClassContext : IClassContext
    {
        public ClassAnnotation ClassAnnotation { get; set; }

        ClassAnnotation IClassContext.Annotation => this.ClassAnnotation;

        IClassContext IHasClassContext.ClassContext_N003 => this;
    }
}
