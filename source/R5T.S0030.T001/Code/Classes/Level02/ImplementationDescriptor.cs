using System;


namespace R5T.S0030.T001.Level02
{
    /// <summary>
    /// Describes a class in terms relevant to being a service implementation.
    /// This is the most advanced level, containing information directly relevant to being a service implementation.
    /// </summary>
    public class ImplementationDescriptor
    {
        public string NamespacedTypeName { get; set; }

        // Make explicit.
        public bool HasServiceDefinition { get; set; }
        public string ServiceDefinitionNamespacedTypeName { get; set; }

        public bool HasServiceDependencies { get; set; }
        public string[] ServiceDependencyNamespacedTypeNames { get; set; }
    }
}
