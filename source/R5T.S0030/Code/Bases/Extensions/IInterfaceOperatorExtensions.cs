using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.F0001.F002;
using R5T.T0045;


namespace R5T.S0030
{
    public static class IInterfaceOperatorExtensions
    {
        public static bool HasR5T_T0064_ServiceDefinitionMarkerAttribute(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration,
            CompilationUnitSyntax compilationUnit)
        {
            var output = Instances.Operation.HasAttributeOfNamespacedTypeName(
                interfaceDeclaration,
                compilationUnit,
                Instances.NamespacedTypeName.R5T_T0064_ServiceDefinitionMarkerAttribute());

            return output;
        }

        public static bool HasServiceDefinitionMarkerAttribute(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration)
        {
            var output = interfaceDeclaration.HasAttributeOfType(Instances.TypeName.ServiceDefinitionMarkerAttribute());
            return output;
        }

        public static bool HasServiceDefinitionMarkerInterface(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration)
        {
            var output = interfaceDeclaration.HasBaseTypeWithNamespacedTypeName(
                Instances.NamespacedTypeName.R5T_T0064_IServiceDefinition());

            return output;
        }

        /// <summary>
        /// An interface is a service definition when it has the service definition marker attribute.
        /// Note: the service definition marker interface is not required (only suggested).
        /// </summary>
        public static bool IsServiceDefinition(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration)
        {
            var output = _.HasServiceDefinitionMarkerAttribute(interfaceDeclaration);
            return output;
        }

        public static bool LacksR5T_T0064_ServiceDefinitionMarkerAttribute(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration,
            CompilationUnitSyntax compilationUnit)
        {
            var hasAttribute = _.HasR5T_T0064_ServiceDefinitionMarkerAttribute(
                interfaceDeclaration,
                compilationUnit);

            var output = !hasAttribute;
            return output;
        }

        public static bool LacksServiceDefinitionMarkerAttribute(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration)
        {
            var hasServiceDefinitionMarkerAttribute = _.HasServiceDefinitionMarkerAttribute(interfaceDeclaration);

            var output = !hasServiceDefinitionMarkerAttribute;
            return output;
        }

        public static bool LacksServiceDefinitionMarkerInterface(this IInterfaceOperator _,
            InterfaceDeclarationSyntax interfaceDeclaration)
        {
            var hasServiceDefinitionMarkerInterface = _.HasServiceDefinitionMarkerInterface(interfaceDeclaration);

            var output = !hasServiceDefinitionMarkerInterface;
            return output;
        }
    }
}
