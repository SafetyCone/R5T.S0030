using System;


namespace R5T.S0030
{
    public class ServiceImplementationTypeDescriptor : IServiceImplementationTypeDescriptor
    {
        public string ServiceDefinitionTypeName { get; set; }
        public string[] DependencyServiceDefinitionTypeNames { get; set; }
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
    }
}
