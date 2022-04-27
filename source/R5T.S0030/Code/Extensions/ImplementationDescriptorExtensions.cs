using System;
using System.Linq;

using R5T.S0030.T001.Level02;


namespace R5T.S0030
{
    public static class ImplementationDescriptorExtensions
    {
        public static string[] GetRequiredNamespaces(this ImplementationDescriptor serviceImplementation)
        {
            // Start by adding namespaces.
            var output = new[]
            {
                Instances.NamespacedTypeNameOperator.GetNamespaceName(serviceImplementation.NamespacedTypeName),
                Instances.NamespacedTypeNameOperator.GetNamespaceName(serviceImplementation.ServiceDefinitionNamespacedTypeName),
            }
            // Service dependency namespaces.
            .Append(
                serviceImplementation.ServiceDependencyNamespacedTypeNames
                    .Select(x => Instances.NamespacedTypeNameOperator.GetNamespaceName(x)))
            .Now();

            return output;
        }
    }
}
