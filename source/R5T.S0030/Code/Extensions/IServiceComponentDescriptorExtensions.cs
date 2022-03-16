using System;


namespace R5T.S0030
{
    public static class IServiceComponentDescriptorExtensions
    {
        public static ReasonedServiceComponentDescriptor GetReasonedServiceComponentDescriptor(this IServiceComponentDescriptor serviceComponentDescriptor,
            string reason)
        {
            var output = new ReasonedServiceComponentDescriptor
            {
                CodeFilePath = serviceComponentDescriptor.CodeFilePath,
                ProjectFilePath = serviceComponentDescriptor.ProjectFilePath,
                Reason = reason,
                TypeName = serviceComponentDescriptor.TypeName,
            };

            return output;
        }
    }
}
