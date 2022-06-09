using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;
using R5T.T0126;

using R5T.S0030.T003;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;


namespace R5T.S0030.Library
{
    public static class ITypeOperatorExtensions
    {
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0060 // Remove unused parameter
        public static Func<CompilationUnitSyntax, T, Task<CompilationUnitSyntax>> AddProperty<T>(this ITypeOperator __,
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore IDE0079 // Remove unnecessary suppression
            string propertyTypeNamespacedTypeName,
            string propertyName)
            where T : IHasTypeAnnotation, N001.IHasCompilationUnitContext, N002.IHasNamespaceContext
        {
            Task<CompilationUnitSyntax> Internal(CompilationUnitSyntax compilationUnit, T hasContext)
            {
                (compilationUnit, _) = hasContext.AddProperty(
                    compilationUnit,
                    propertyTypeNamespacedTypeName,
                    propertyName);

                return Task.FromResult(compilationUnit);
            }

            return Internal;
        }
    }
}
