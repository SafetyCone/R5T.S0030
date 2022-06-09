using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using N013 = R5T.S0030.T003.N013;


namespace R5T.S0030.Library
{
    public static class IInterfaceOperatorExtensions
    {
        public static Task<CompilationUnitSyntax> NoModifications(this IInterfaceOperator _,
            CompilationUnitSyntax compilationUnit,
#pragma warning disable IDE0060 // Remove unused parameter
            // Required for to match context modifier signature.
            N013.IInterfaceContext context)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            // Do nothing.

            return Task.FromResult(compilationUnit);
        }
    }
}
