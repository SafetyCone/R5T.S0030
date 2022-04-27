using System;

using R5T.Magyar;


namespace R5T.S0030.T003.N001
{
    public class CompilationUnitContextOptions
    {
        public bool AddUsingNamespace_System { get; set; }
        public bool AddUsingNamespace_System_Threading_Tasks { get; set; }
        public bool FormatUsings { get; set; }
        public Override<string> CompilationUnitLocalNamespaceNameOverride { get; set; }
    }
}
