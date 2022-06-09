using System;

using R5T.Magyar;

using R5T.B0006;


namespace R5T.S0030.T003.N001
{
    public static class ICompilationUnitOperatorExtensions
    {
        public static void NoNamespaceAdditions(this ICompilationUnitOperator _,
            CompilationUnitContextOptions options)
        {
            options.AddUsingNamespace_System = false;
            options.AddUsingNamespace_System_Threading_Tasks = false;
        }

        public static void NoSystem_Threading_Tasks(this ICompilationUnitOperator _,
            CompilationUnitContextOptions options)
        {
            options.AddUsingNamespace_System_Threading_Tasks = false;
        }

        public static void SetDefaults(this ICompilationUnitOperator _,
            CompilationUnitContextOptions options)
        {
            options.AddUsingNamespace_System = true;
            options.AddUsingNamespace_System_Threading_Tasks = true;
            options.FormatUsings = true;
            options.CompilationUnitLocalNamespaceNameOverride = Override.NotOverridden<string>();
        }
    }
}
