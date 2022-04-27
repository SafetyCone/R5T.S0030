using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.D0116;


namespace R5T.S0030.T003.N001
{
    public static class CompilationUnitContextOptionsExtensions
    {
        public static async Task<CompilationUnitSyntax> Apply(this CompilationUnitContextOptions options,
            CompilationUnitSyntax compilationUnit,
            IUsingDirectivesFormatter usingDirectivesFormatter)
        {
            if(options.AddUsingNamespace_System)
            {
                compilationUnit = compilationUnit.AddUsing(
                    Instances.NamespaceName.System());
            }

            if (options.AddUsingNamespace_System_Threading_Tasks)
            {
                compilationUnit = compilationUnit.AddUsing(
                    Instances.NamespaceName.System_Threading_Tasks());
            }

            if (options.FormatUsings)
            {
                var compilationUnitLocalNamespaceName = options.CompilationUnitLocalNamespaceNameOverride.IsOverridden
                    ? options.CompilationUnitLocalNamespaceNameOverride.Value
                    : compilationUnit.GetFirstNamespaceName()
                    ;

                compilationUnit = await usingDirectivesFormatter.FormatUsingDirectives(
                    compilationUnit,
                    compilationUnitLocalNamespaceName);
            }

            return compilationUnit;
        }

        [return: MutableFluency]
        public static CompilationUnitContextOptions SetDefaults(this CompilationUnitContextOptions options)
        {
            options.AddUsingNamespace_System = true;
            options.AddUsingNamespace_System_Threading_Tasks = true;
            options.FormatUsings = true;
            options.CompilationUnitLocalNamespaceNameOverride = Override.NotOverridden<string>();

            return options;
        }
    }
}
