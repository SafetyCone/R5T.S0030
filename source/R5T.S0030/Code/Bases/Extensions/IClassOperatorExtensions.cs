using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.F0001.F002;
using R5T.T0045;


namespace R5T.S0030
{
    public static class IClassOperatorExtensions
    {
        public static bool HasR5T_T0064_ServiceImplementationMarkerAttribute(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit)
        {
            var output = Instances.Operation.HasAttributeOfNamespacedTypeName(
                classDeclaration,
                compilationUnit,
                Instances.NamespacedTypeName.R5T_T0064_ServiceImplementationMarkerAttribute());

            return output;
        }

        public static bool HasServiceImplementationMarkerAttribute(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var output = classDeclaration.HasAttributeOfType(Instances.TypeName.ServiceImplementationMarkerAttribute());
            return output;
        }

        public static bool HasServiceImplementationMarkerInterface(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit)
        {
            var output = Instances.Operation.HasBaseTypeWithNamespacedTypeName(
                classDeclaration,
                compilationUnit,
                Instances.NamespacedTypeName.R5T_T0064_IServiceImplementation());

            return output;
        }

        public static bool HasServiceImplementationMarkerInterface(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var compilationUnit = classDeclaration.GetContainingCompilationUnit();

            var output = _.HasServiceImplementationMarkerInterface(
                classDeclaration,
                compilationUnit);

            return output;
        }

        /// <summary>
        /// A class is a service implementation when it has the service implementation marker attribute.
        /// Note: the service implementation marker interface is not required (only suggested).
        /// </summary>
        public static bool IsServiceImplementation(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var output = true
                && _.HasServiceImplementationMarkerAttribute(classDeclaration)
                // Service implementations are not abstract, and are not static.
                && !classDeclaration.IsAbstract()
                && !classDeclaration.IsStatic()
                ;

            return output;
        }

        public static bool LacksServiceDefinitionMarkerAttribute(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var hasServiceImplementationMarkerAttribute = _.HasServiceImplementationMarkerAttribute(classDeclaration);

            var output = !hasServiceImplementationMarkerAttribute;
            return output;
        }

        public static bool LacksServiceDefinitionMarkerInterface(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var hasServiceImplementationMarkerInterface = _.HasServiceImplementationMarkerInterface(classDeclaration);

            var output = !hasServiceImplementationMarkerInterface;
            return output;
        }

        public static bool LacksR5T_T0064_ServiceImplementationMarkerAttribute(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit)
        {
            var hasAttribute = _.HasR5T_T0064_ServiceImplementationMarkerAttribute(
                classDeclaration,
                compilationUnit);

            var output = !hasAttribute;
            return output;
        }
    }
}
