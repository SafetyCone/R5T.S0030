using System;

using R5T.T0092;


namespace R5T.S0030
{
    /// <summary>
    /// Used as a unique information interchange type for describing service implementations.
    /// </summary>
    public class ServiceImplementationDescriptor : INamed
    {
        public string NamespacedTypeName { get; set; }

        // Make explicit.
        public bool HasServiceDefinition { get; set; }
        public string ServiceDefinitionNamespacedTypeName { get; set; }

        public bool HasServiceDependencies { get; set; }
        public string[] ServiceDependencyNamespacedTypeNames { get; set; }

        public string CodeFilePath { get; set; }
        public string ProjectFilePath { get; set; }

        string INamed.Name => this.NamespacedTypeName;
    }
}
