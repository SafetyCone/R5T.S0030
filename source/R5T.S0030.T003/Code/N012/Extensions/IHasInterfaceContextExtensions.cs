using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.S0030.T003.N012
{
    using InterfaceModifierAction = Func<CompilationUnitSyntax, IInterfaceContext, Task<CompilationUnitSyntax>>;


    public static class IHasInterfaceContextExtensions
    {
        public static async Task<CompilationUnitSyntax> Modify(this IHasInterfaceContext hasContext,
            Func<IInterfaceContext, Task<CompilationUnitSyntax>> interfaceCompilationUnitGenerator)
        {
            // Run modifier.
            var compilationUnit = await interfaceCompilationUnitGenerator(
                hasContext.InterfaceContext_N012);

            // Perform after modification actions.
            compilationUnit = await hasContext.RunAfterModificationActions(
                compilationUnit);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> Modify(this IHasInterfaceContext hasContext,
            CompilationUnitSyntax compilationUnit,
            InterfaceModifierAction afterAdditionClassModifierAction)
        {
            // Run modifier.
            compilationUnit = await afterAdditionClassModifierAction(
                compilationUnit,
                hasContext.InterfaceContext_N012);

            // Perform after modification actions.
            compilationUnit = await hasContext.RunAfterModificationActions(
                compilationUnit);

            return compilationUnit;
        }

        private static Task<CompilationUnitSyntax> RunAfterModificationActions(this IHasInterfaceContext _,
            CompilationUnitSyntax compilationUnit)
        {
            // NONE.

            return Task.FromResult(compilationUnit);
        }
    }
}
