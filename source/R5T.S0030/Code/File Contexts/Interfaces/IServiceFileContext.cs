using System;


namespace R5T.S0030.FileContexts
{
    public interface IServiceFileContext
    {
        FileSet<Entities.ServiceDefinition> ServiceDefinitions { get; }
    }
}
