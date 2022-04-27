using System;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Describes a dependency type in terms relevant to being a service definition dependency of a service implementation.
    /// </summary>
    public class ServiceDefinitionDependencyDescriptor
    {
        public string TypeNameFragment { get; set; }
        // Make it explicit, even though implicitly a null or empty service definition namespaced type name could communicate the same information.
        public bool IsRecognizedServiceDefinitionType { get; set; }
        public string ServiceDefinitionNamespacedTypeName { get; set; }
    }
}
