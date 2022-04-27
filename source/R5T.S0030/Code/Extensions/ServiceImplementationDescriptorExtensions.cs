using System;

using R5T.S0030.T001.Level02;

using R5T.S0030;


namespace System
{
    public static class ServiceImplementationDescriptorExtensions
    {
        public static ImplementationDescriptor ToImplementationDescriptor_Level02(this ServiceImplementationDescriptor serviceImplementationDescriptor)
        {
            var output = new ImplementationDescriptor
            {
                HasServiceDefinition = serviceImplementationDescriptor.HasServiceDefinition,
                HasServiceDependencies = serviceImplementationDescriptor.HasServiceDependencies,
                NamespacedTypeName = serviceImplementationDescriptor.NamespacedTypeName,
                ServiceDefinitionNamespacedTypeName = serviceImplementationDescriptor.ServiceDefinitionNamespacedTypeName,
                ServiceDependencyNamespacedTypeNames = serviceImplementationDescriptor.ServiceDependencyNamespacedTypeNames,
            };

            return output;
        }
    }
}
