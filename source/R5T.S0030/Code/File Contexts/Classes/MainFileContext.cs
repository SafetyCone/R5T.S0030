using System;
using System.Collections.Generic;

using R5T.T0128;


namespace R5T.S0030.FileContexts
{
    public class MainFileContext : FileContext,
        IServiceRepositoryFileContext
    {
        public FileSet<D0101.I001.Entities.Project> Projects { get; set; }
        public FileSet<Entities.ServiceComponentToProjectMapping> ServiceComponentToProjectMappings { get; set; }
        public FileSet<Entities.ServiceDefinition> ServiceDefinitions { get; set; }
        public FileSet<Entities.ServiceImplementation> ServiceImplementations { get; set; }
        public FileSet<Entities.ImplementionToDefinitionMapping> ToDependencyDefinitionMappings { get; set; }
        public FileSet<Entities.ImplementionToDefinitionMapping> ToImplementedDefinitionMappings { get; set; }


        protected override IEnumerable<FileSet> GetAllFileSets()
        {
            return EnumerableHelper.From<FileSet>(
                this.Projects,
                this.ServiceComponentToProjectMappings,
                this.ServiceDefinitions,
                this.ServiceImplementations,
                this.ToDependencyDefinitionMappings,
                this.ToImplementedDefinitionMappings);
        }
    }
}
