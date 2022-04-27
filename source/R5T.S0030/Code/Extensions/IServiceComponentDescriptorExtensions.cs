using System;
using System.Collections.Generic;
using System.Linq;


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

        public static Repositories.ServiceComponentDataSet GetRepositoryServiceComponentDataSet(this IServiceComponentDescriptor serviceDefinitionDescriptor)
        {
            var output = new Repositories.ServiceComponentDataSet
            {
                CodeFilePath = serviceDefinitionDescriptor.CodeFilePath,
                TypeName = serviceDefinitionDescriptor.TypeName,
            };

            return output;
        }

        public static IEnumerable<Repositories.ServiceComponentDataSet> GetRepositoryServiceComponentDataSets(this IEnumerable<IServiceComponentDescriptor> serviceDefinitionDescriptors)
        {
            var output = serviceDefinitionDescriptors
                .Select(x => x.GetRepositoryServiceComponentDataSet())
                ;

            return output;
        }
    }
}
