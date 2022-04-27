using System;


namespace R5T.S0030
{
    public class ServiceImplementationTypeDescriptor : TypeNamedCodeFilePathed, IServiceImplementationTypeDescriptor
    {
        public string ServiceDefinitionTypeName { get; set; }
        public string[] DependencyServiceDefinitionTypeNames { get; set; }
    }
}
