using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.X000.N8; // For SyntaxTrivia.ToSyntaxTriviaList() extension.
using R5T.T0126;

using R5T.S0030.T003;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;


namespace System
{
    public static class IHasBaseTypeAnnotationExtensions
    {
        public static CompilationUnitSyntax AddBaseTypeByNamespacedTypeName<T>(this T hasContext,
            CompilationUnitSyntax compilationUnit,
            string baseTypeNamespacedTypeName)
            where T : N001.IHasCompilationUnitContext, N002.IHasNamespaceContext, IHasBaseTypeAnnotation
        {
            var namespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(baseTypeNamespacedTypeName);
            var baseTypeTypeName = Instances.NamespacedTypeNameOperator.GetTypeName(baseTypeNamespacedTypeName);

            compilationUnit = hasContext.AddUsings(
                compilationUnit,
                namespaceName);

            compilationUnit = hasContext.BaseTypeAnnotation.ModifySynchronous(
                compilationUnit,
                baseType =>
                {
                    // Add base type.
                    var baseListAnnotation = SyntaxNodeAnnotation.Initialize<BaseListSyntax>(); // Currently required since "Mixed declarations and expressions in deconstruction is currently in Preview..."
                    (baseType, baseListAnnotation) = baseType.AcquireBaseList();

                    var baseTypeAnnotation = SyntaxNodeAnnotation.Initialize<BaseTypeSyntax>(); // Required to simplify getting the base type annotation out of the inner lambda context.
                    baseType = baseType.Modify_TypedSynchronous(
                        baseListAnnotation,
                        baseList =>
                        {
                            (baseList, baseTypeAnnotation) = baseList.AddBaseType(
                                baseTypeTypeName);

                            baseList = baseList.SetLeadingSeparatingSpacing(
                                baseTypeAnnotation,
                                SyntaxTriviaHelper.Space().ToSyntaxTriviaList());

                            return baseList;
                        });

                    return baseType;
                });

            return compilationUnit;
        }
    }
}
