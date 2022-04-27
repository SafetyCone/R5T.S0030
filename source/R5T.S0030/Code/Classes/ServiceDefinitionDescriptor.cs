using System;

using R5T.T0092;
using R5T.T0093;


namespace R5T.S0030
{
    public class ServiceDefinitionDescriptor : IServiceDefinitionDescriptor
    {
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
        public string ProjectFilePath { get; set; }

        string INamed.Name => this.TypeName;
        string IFilePathed.FilePath => this.CodeFilePath;


        public override string ToString()
        {
            var representation = this.TypeName;
            return representation;
        }
    }
}
