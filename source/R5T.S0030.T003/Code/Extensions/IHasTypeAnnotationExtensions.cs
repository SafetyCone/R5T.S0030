using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
//using N013 = R5T.S0030.T003.N013;

using SyntaxFactory = R5T.L0011.X000.Generation.Initial.Simple.SyntaxFactory;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IHasTypeAnnotationExtensions
    {
        public static (CompilationUnitSyntax, ISyntaxNodeAnnotation<PropertyDeclarationSyntax>) AddProperty<T>(this T hasContext,
            CompilationUnitSyntax compilationUnit,
            string propertyTypeNamespacedTypeName,
            string propertyName)
            where T : IHasTypeAnnotation, N001.IHasCompilationUnitContext, N002.IHasNamespaceContext
        {
            var propertyTypeNamespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(propertyTypeNamespacedTypeName);
            var propertyTypeName = Instances.NamespacedTypeNameOperator.GetTypeName(propertyTypeNamespacedTypeName);

            compilationUnit = hasContext.AddUsings(
                compilationUnit,
                propertyTypeNamespaceName);

            var propertyAnnotation = SyntaxNodeAnnotation.Initialize<PropertyDeclarationSyntax>();

            compilationUnit = hasContext.TypeAnnotation.ModifySynchronous(
                compilationUnit,
                type =>
                {
                    // Add property.
                    var property = SyntaxFactory.CreateProperty(propertyTypeName, propertyName)
                        .AsAnnotatedNode()
                        .Modify(property =>
                        {
                            var getAccessor = SyntaxFactory.CreateAccessorGet();

                            property = property.AddAccessorListAccessors(getAccessor);

                            return property;
                        })
                        .NormalizeWhitespace()
                        .Modify(property => property.WithLeadingTrivia(
                            type.GetLineIndentation().IndentByTab()))
                        ;

                    propertyAnnotation = property.Annotation;

                    type = type.AddProperty_Simple(property);

                    return type;
                });

            return (compilationUnit, propertyAnnotation);
        }
    }
}
