using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T003.N002;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IHasNamespaceContextExtensions
    {
        public static async Task<CompilationUnitSyntax> Modify(this IHasNamespaceContext hasNamespaceContext,
            Func<INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            // Run modifier.
            var compilationUnit = await afterAdditionNamespaceModifierAction(hasNamespaceContext.NamespaceContext_N002);

            // Do after modification actions.
            compilationUnit = await hasNamespaceContext.RunAfterModificationActions(
                compilationUnit);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> Modify(this IHasNamespaceContext hasNamespaceContext,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            // Run modifier.
            compilationUnit = await afterAdditionNamespaceModifierAction(
                compilationUnit,
                hasNamespaceContext.NamespaceContext_N002);

            // Do after modification actions.
            compilationUnit = await hasNamespaceContext.RunAfterModificationActions(
                compilationUnit);

            return compilationUnit;
        }

        private static Task<CompilationUnitSyntax> RunAfterModificationActions(this IHasNamespaceContext hasCamespaceContext,
            CompilationUnitSyntax compilationUnit)
        {
            compilationUnit = Instances.NamespaceOperator.SetEndBraceLineIndentation(
                compilationUnit,
                hasCamespaceContext.NamespaceContext_N002.Annotation);
            // TODO, format namespace usings.

            return Task.FromResult(compilationUnit);
        }
    }
}
