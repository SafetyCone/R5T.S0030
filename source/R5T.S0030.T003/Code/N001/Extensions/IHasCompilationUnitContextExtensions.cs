using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T003.N001;


namespace System
{
    public static class IHasCompilationUnitContextExtensions
    {
        public static CompilationUnitSyntax AddUsings(this IHasCompilationUnitContext _,
            CompilationUnitSyntax compilationUnit,
            params string[] namespaceNames)
        {
            var output = compilationUnit.AddUsings(namespaceNames);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this IHasCompilationUnitContext _,
            CompilationUnitSyntax compilationUnit,
            params (string DestinationName, string SourceNameExpression)[] nameAliasValues)
        {
            var output = compilationUnit.AddUsings(nameAliasValues);
            return output;
        }

        public static async Task<CompilationUnitSyntax> Modify(this IHasCompilationUnitContext hasCompilationUnitContext,
            Func<ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitConstructor,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            // Get default options, then modify.
            var options = new CompilationUnitContextOptions()
                .SetDefaults();

            FunctionHelper.Run(optionsModifierAction, options);

            // Run constructor.
            var compilationUnit = await compilationUnitConstructor(
                hasCompilationUnitContext.CompilationUnitContext_N001);

            // Do after modification actions.
            compilationUnit = await hasCompilationUnitContext.RunAfterModificationActions(
                compilationUnit,
                options);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> Modify(this IHasCompilationUnitContext hasCompilationUnitContext,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            // Get default options, then modify.
            var options = new CompilationUnitContextOptions()
                .SetDefaults();

            FunctionHelper.Run(optionsModifierAction, options);

            // Run modifier.
            compilationUnit = await compilationUnitModifierAction(
                compilationUnit,
                hasCompilationUnitContext.CompilationUnitContext_N001);

            // Do after modification actions.
            compilationUnit = await hasCompilationUnitContext.RunAfterModificationActions(
                compilationUnit,
                options);

            return compilationUnit;
        }

        private static async Task<CompilationUnitSyntax> RunAfterModificationActions(this IHasCompilationUnitContext hasCompilationUnitContext,
            CompilationUnitSyntax compilationUnit,
            CompilationUnitContextOptions options)
        {
            compilationUnit = await options.Apply(
                compilationUnit,
                hasCompilationUnitContext.CompilationUnitContext_N001.UsingDirectivesFormatter);

            return compilationUnit;
        }
    }
}
