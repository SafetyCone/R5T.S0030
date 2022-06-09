using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IHasMemberAnnotationExtensions
    {
        public static CompilationUnitSyntax AddAttributeByNamespacedTypeName<T>(this T hasContext,
            CompilationUnitSyntax compilationUnit,
            string attributeNamespacedTypeName)
            where T : N001.IHasCompilationUnitContext, N002.IHasNamespaceContext, IHasMemberAnnotation
        {
            var namespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(attributeNamespacedTypeName);
            var attributeTypeName = Instances.NamespacedTypeNameOperator.GetTypeName(attributeNamespacedTypeName);

            compilationUnit = hasContext.AddUsings(compilationUnit,
                new[]
                {
                    namespaceName,
                });

            compilationUnit = hasContext.MemberAnnotation.ModifySynchronous(
                compilationUnit,
                member =>
                {
                    // Add attribute.
                    member = member.AddAttributeByAttributeName(
                        attributeTypeName);

                    return member;
                });

            return compilationUnit;
        }

        //public static CompilationUnitSyntax AddModifier<T>(this T hasContext,
        //    CompilationUnitSyntax compilationUnit,
        //    string modifier)
        //    where T : IHasMemberAnnotation
        //{
        //    compilationUnit = hasContext.MemberAnnotation.ModifySynchronous(
        //        compilationUnit,
        //        member =>
        //        {
        //            member.
        //        });

        //    return compilationUnit;
        //}

        /// <summary>
        /// Makes the member static and returns an annotation to the static modifier token.
        /// </summary>
        public static (CompilationUnitSyntax, ISyntaxTokenAnnotation) MakeStatic<T>(this T hasContext,
            CompilationUnitSyntax compilationUnit)
            where T : IHasMemberAnnotation
        {
            var staticTokenAnnotation = SyntaxTokenAnnotation.Initialize();

            compilationUnit = hasContext.MemberAnnotation.ModifySynchronous(
                compilationUnit,
                member =>
                {
                    (member, staticTokenAnnotation) = member.MakeStatic();

                    return member;
                });

            return (compilationUnit, staticTokenAnnotation);
        }

        public static (CompilationUnitSyntax, ISyntaxTokenAnnotation) MakePartial<T>(this T hasContext,
            CompilationUnitSyntax compilationUnit)
            where T : IHasMemberAnnotation
        {
            var partialTokenAnnotation = SyntaxTokenAnnotation.Initialize();

            compilationUnit = hasContext.MemberAnnotation.ModifySynchronous(
                compilationUnit,
                member =>
                {
                    (member, partialTokenAnnotation) = member.MakePartial();

                    return member;
                });

            return (compilationUnit, partialTokenAnnotation);
        }
    }
}
