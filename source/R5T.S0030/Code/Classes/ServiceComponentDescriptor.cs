using System;


namespace R5T.S0030
{
    public class ServiceComponentDescriptor : TypeNamedCodeFilePathed, IServiceComponentDescriptor
    {
        public string ProjectFilePath { get; set; }
    }
}
