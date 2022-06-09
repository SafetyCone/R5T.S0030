using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Extensions;

using R5T.L0011.X000.N8; // For SyntaxTrivia.ToSyntaxTriviaList() extension.
using R5T.T0126;

using R5T.S0030.T003.N013;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IHasInterfaceContextExtensions
    {
        //public static CompilationUnitSyntax AddAttributeByNamespacedTypeName(this IHasInterfaceContext hasContext,
        //    CompilationUnitSyntax compilationUnit,
        //    string attributeNamespacedTypeName)
        //{
        //    var interfaceContext = hasContext.InterfaceContext_N013;

        //    var namespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(attributeNamespacedTypeName);
        //    var attributeTypeName = Instances.NamespacedTypeNameOperator.GetTypeName(attributeNamespacedTypeName);

        //    compilationUnit = interfaceContext.AddUsings(compilationUnit,
        //        new[]
        //        {
        //            namespaceName,
        //        });

        //    compilationUnit = interfaceContext.InterfaceAnnotation.ModifySynchronous(
        //        compilationUnit,
        //        @interface =>
        //        {
        //            // Add attribute.
        //            @interface = @interface.AddAttributeByAttributeName(
        //                attributeTypeName);

        //            return @interface;
        //        });

        //    return compilationUnit;
        //}

        //public static CompilationUnitSyntax AddBaseTypeByNamespacedTypeName(this IHasInterfaceContext hasContext,
        //    CompilationUnitSyntax compilationUnit,
        //    string baseTypeNamespacedTypeName)
        //{
        //    var interfaceContext = hasContext.InterfaceContext_N013;

        //    var namespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(baseTypeNamespacedTypeName);
        //    var baseTypeTypeName = Instances.NamespacedTypeNameOperator.GetTypeName(baseTypeNamespacedTypeName);

        //    compilationUnit = interfaceContext.AddUsings(compilationUnit,
        //        new[]
        //        {
        //            namespaceName,
        //        });

        //    compilationUnit = interfaceContext.InterfaceAnnotation.ModifySynchronous(
        //        compilationUnit,
        //        @interface =>
        //        {
        //            // Add base type.
        //            var baseListAnnotation = SyntaxNodeAnnotation.Initialize<BaseListSyntax>(); // Currently required since "Mixed declarations and expressions in deconstruction is currently in Preview..."
        //            (@interface, baseListAnnotation) = @interface.AcquireBaseList();

        //            var baseTypeAnnotation = SyntaxNodeAnnotation.Initialize<BaseTypeSyntax>(); // Required to simplify getting the base type annotation out of the inner lambda context.
        //            @interface = @interface.Modify_TypedSynchronous(
        //                baseListAnnotation,
        //                baseList =>
        //                {
        //                    (baseList, baseTypeAnnotation) = baseList.AddBaseType(
        //                        baseTypeTypeName);

        //                    baseList = baseList.SetLeadingSeparatingSpacing(
        //                        baseTypeAnnotation,
        //                        SyntaxTriviaHelper.Space().ToSyntaxTriviaList());

        //                    return baseList;
        //                });

        //            return @interface;
        //        });

        //    return compilationUnit;
        //}
    }
}
