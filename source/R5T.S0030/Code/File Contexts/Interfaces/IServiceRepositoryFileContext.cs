using System;

using R5T.D0101.I001;
using R5T.T0128;


namespace R5T.S0030.FileContexts
{
    public interface IServiceRepositoryFileContext : IProjectRepositoryFileContext
    {
        FileSet<Entities.ServiceComponentToProjectMapping> ServiceComponentToProjectMappings { get; }
        FileSet<Entities.ServiceDefinition> ServiceDefinitions { get; }
        FileSet<Entities.ServiceImplementation> ServiceImplementations { get; }
        FileSet<Entities.ImplementionToDefinitionMapping> ToImplementedDefinitionMappings { get; }
        FileSet<Entities.ImplementionToDefinitionMapping> ToDependencyDefinitionMappings { get; }
    }
}
