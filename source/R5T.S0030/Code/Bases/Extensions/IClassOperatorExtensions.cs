using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;


namespace R5T.S0030
{
    public static class IClassOperatorExtensions
    {
        public static bool HasServiceImplementationMarkerAttribute(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var output = classDeclaration.HasAttributeOfType(Instances.TypeName.ServiceImplementationMarkerAttribute());
            return output;
        }

        public static bool HasServiceImplementationMarkerInterface(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var output = classDeclaration.HasBaseTypeWithName(Instances.TypeName.IServiceImplementation());
            return output;
        }

        /// <summary>
        /// A class is a service implementation when it has the service implementation marker attribute.
        /// Note: the service implementation marker interface is not required (only suggested).
        /// </summary>
        public static bool IsServiceImplementation(this IClassOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var output = _.HasServiceImplementationMarkerAttribute(classDeclaration);
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
    }
}
