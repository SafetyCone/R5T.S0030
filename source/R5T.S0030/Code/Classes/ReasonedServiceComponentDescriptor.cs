using System;


namespace R5T.S0030
{
    public class ReasonedServiceComponentDescriptor : ServiceComponentDescriptor, IReasonedServiceComponentDescriptor
    {
        public string Reason { get; set; }
    }
}
