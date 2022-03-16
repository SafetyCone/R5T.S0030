using System;

using R5T.S0030.Repositories;


namespace R5T.S0030.FileContexts.Entities
{
    public class ServiceDefinition : IServiceDefinition
    {
        public Guid Identity { get; set; }
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
    }
}
