using System;


namespace R5T.S0030
{
    public static class TypeNamedCodeFilePathExtensions
    {
        public static ServiceComponentDescriptor GetServiceComponentDescriptor(this ITypeNamedCodeFilePathed typeNamedCodeFilePath,
            string projectFilePath)
        {
            var output = new ServiceComponentDescriptor
            {
                CodeFilePath = typeNamedCodeFilePath.CodeFilePath,
                ProjectFilePath = projectFilePath,
                TypeName = typeNamedCodeFilePath.TypeName,
            };

            return output;
        }

        public static ServiceDefinitionDescriptor GetServiceDefinitionDescriptor(this ITypeNamedCodeFilePathed typeNamedCodeFilePath,
            string projectFilePath)
        {
            var output = new ServiceDefinitionDescriptor
            {
                CodeFilePath = typeNamedCodeFilePath.CodeFilePath,
                ProjectFilePath = projectFilePath,
                TypeName = typeNamedCodeFilePath.TypeName,
            };

            return output;
        }

        public static ReasonedServiceComponentDescriptor GetReasonedServiceComponentDescriptor(this ITypeNamedCodeFilePathed typeNamedCodeFilePath,
            string projectFilePath,
            string reason)
        {
            var output = new ReasonedServiceComponentDescriptor
            {
                CodeFilePath = typeNamedCodeFilePath.CodeFilePath,
                ProjectFilePath = projectFilePath,
                Reason = reason,
                TypeName = typeNamedCodeFilePath.TypeName,
            };

            return output;
        }
    }
}
