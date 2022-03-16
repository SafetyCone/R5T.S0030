using System;


namespace R5T.S0030
{
    public class ServiceComponentDescriptor : IServiceComponentDescriptor
    {
        public string ProjectFilePath { get; set; }
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
    }
}
