using System;


namespace R5T.S0030
{
    public static class IServiceDefinitionDescriptorExtensions
    {
        public static Repositories.ServiceDefinitionDescriptor GetRepositoryServiceDefinitionDescriptor(this IServiceDefinitionDescriptor serviceDefinitionDescriptor)
        {
            var output = new Repositories.ServiceDefinitionDescriptor
            {
                CodeFilePath = serviceDefinitionDescriptor.CodeFilePath,
                TypeName = serviceDefinitionDescriptor.TypeName,
            };

            return output;
        }
    }
}
