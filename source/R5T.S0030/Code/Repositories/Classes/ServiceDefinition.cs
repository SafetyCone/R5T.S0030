using System;


namespace R5T.S0030.Repositories
{
    public class ServiceDefinition : IServiceDefinition
    {
        public Guid Identity { get; }
        public string TypeName { get; }
        public string CodeFilePath { get; }
    }
}
