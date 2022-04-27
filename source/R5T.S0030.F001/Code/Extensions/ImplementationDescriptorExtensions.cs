using System;
using System.N0;
using System.Linq;

using ImplementationDescriptor = R5T.S0030.T001.Level02.ImplementationDescriptor;
using TypeNameBasedImplementationDescriptor = R5T.S0030.F001.TypeNameBasedImplementationDescriptor;

using Instances = R5T.S0030.F001.Instances;


namespace System
{
    public static class ImplementationDescriptorExtensions
    {
        public static TypeNameBasedImplementationDescriptor GetTypeNameBasedIMplementationDescriptor(this ImplementationDescriptor implementationDescriptor)
        {
            // Get type names.
            var definitionNamespacedTypeName = implementationDescriptor.ServiceDefinitionNamespacedTypeName;
            var definitionTypeName = Instances.TypeNameOperator.RemoveGenericTypeParameterArityFromTypeName(
                Instances.NamespacedTypeNameOperator.GetTypeName(definitionNamespacedTypeName));
            // TODO, handle case where ther is no service definition.

            var implementationNamespacedTypeName = implementationDescriptor.NamespacedTypeName;
            var implementationTypeName = Instances.TypeNameOperator.RemoveGenericTypeParameterArityFromTypeName(
                Instances.NamespacedTypeNameOperator.GetTypeName(implementationNamespacedTypeName));

            var dependencyDefinitionNamespacedTypeNames = implementationDescriptor.ServiceDependencyNamespacedTypeNames;
            var dependencyDefinitionTypeNames = dependencyDefinitionNamespacedTypeNames
                .Select(x => Instances.TypeNameOperator.RemoveGenericTypeParameterArityFromTypeName(
                    Instances.NamespacedTypeNameOperator.GetTypeName(x)))
                .Now();

            var output = new TypeNameBasedImplementationDescriptor()
            {
                DefinitionTypeName = definitionTypeName,
                ImplementationTypeName = implementationTypeName,
                DependencyTypeNames = dependencyDefinitionTypeNames,
            };

            return output;
        }
    }
}
