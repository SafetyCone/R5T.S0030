using System;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Describes a method parameter in terms relevant to being a constructor parameter of a service implementation.
    /// </summary>
    public class ParameterDescriptor : ServiceDefinitionDependencyDescriptor
    {
        public string ParameterName { get; set; }
        public bool HasExplicitNotServiceComponentAttribute { get; set; }
    }
}
