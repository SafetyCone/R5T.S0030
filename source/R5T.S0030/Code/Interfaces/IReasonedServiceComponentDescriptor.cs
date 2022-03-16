using System;


namespace R5T.S0030
{
    public interface IReasonedServiceComponentDescriptor : IServiceComponentDescriptor
    {
        string Reason { get; }
    }
}
