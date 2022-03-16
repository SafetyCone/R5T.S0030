using System;


namespace R5T.S0030
{
    public class ServiceDefinitionDescriptor : IServiceDefinitionDescriptor
    {
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
        public string ProjectFilePath { get; set; }
    }
}
