using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T003.N002;

using N001 = R5T.S0030.T003.N001;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IHasNamespaceContextExtensions
    {
        // NOTE: Input parent node type *must* be compilation unit since we are adding using declarations, and to do so we need to modify the compilation unit.

        public static CompilationUnitSyntax AddUsings<T>(this T hasContext,
            CompilationUnitSyntax compilationUnit,
            params string[] namespaceNames)
            where T : N001.IHasCompilationUnitContext, IHasNamespaceContext
        {
            // Any namespace names reachable from the class context's own namespace do not need to be added as a using namespace directive.
            var classContextNamespaceName = hasContext.NamespaceContext_N002.NamespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.GetFullName());

            var namespaceNamesToAdd = namespaceNames
                .Where(x => !Instances.NamespaceNameOperator.IsReachableFrom(
                    x,
                    classContextNamespaceName))
                .Now();

            var output = hasContext.CompilationUnitContext_N001.AddUsings(
                compilationUnit,
                namespaceNamesToAdd);

            return output;
        }

        public static CompilationUnitSyntax AddUsings<T>(this T _,
            CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
            where T : N001.IHasCompilationUnitContext, IHasNamespaceContext
        {
            var output = _.AddUsings(
                compilationUnit,
                namespaceNames.ToArray());

            return output;
        }

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
                hasCamespaceContext.NamespaceContext_N002.NamespaceAnnotation);
            // TODO, format namespace usings.

            return Task.FromResult(compilationUnit);
        }
    }
}
