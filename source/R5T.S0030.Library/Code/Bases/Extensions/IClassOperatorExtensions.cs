using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using N004 = R5T.S0030.T003.N004;


namespace R5T.S0030.Library
{
    public static class IClassOperatorExtensions
    {
        public static Task<CompilationUnitSyntax> NoModifications(this IClassOperator _,
            CompilationUnitSyntax compilationUnit,
#pragma warning disable IDE0060 // Remove unused parameter
            // Required for to match context modifier signature.
            N004.IClassContext context)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            // Do nothing.

            return Task.FromResult(compilationUnit);
        }
    }
}
