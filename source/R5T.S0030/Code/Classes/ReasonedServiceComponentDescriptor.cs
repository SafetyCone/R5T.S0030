using System;


namespace R5T.S0030
{
    public class ReasonedServiceComponentDescriptor : IReasonedServiceComponentDescriptor
    {
        public string Reason { get; set; }
        public string ProjectFilePath { get; set; }
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
    }
}
