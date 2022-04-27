using System;

using R5T.T0092;
using R5T.T0093;


namespace R5T.S0030.Repositories
{
    public class ServiceDefinition : IServiceDefinition
    {
        public Guid Identity { get; set; }
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }

        string INamed.Name => this.TypeName;
        string IFilePathed.FilePath => this.CodeFilePath;
    }
}
