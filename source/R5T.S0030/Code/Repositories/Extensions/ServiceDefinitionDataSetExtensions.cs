using System;

using R5T.S0030.Repositories;

using EntityType = R5T.S0030.FileContexts.Entities.ServiceDefinition;


namespace System
{
    public static class ServiceDefinitionDataSetExtensions
    {
        public static EntityType GetEntityType(this ServiceComponentDataSet dataSet,
            Guid identity)
        {
            var output = new EntityType
            {
                CodeFilePath = dataSet.CodeFilePath,
                Identity = identity,
                TypeName = dataSet.TypeName,
            };

            return output;
        }

        public static ServiceDefinition GetServiceDefinition(this ServiceComponentDataSet dataSet,
            Guid identity)
        {
            var output = new ServiceDefinition
            {
                CodeFilePath = dataSet.CodeFilePath,
                Identity = identity,
                TypeName = dataSet.TypeName,
            };

            return output;
        }

        public static ServiceImplementation GetServiceImplementation(this ServiceComponentDataSet dataSet,
            Guid identity)
        {
            var output = new ServiceImplementation
            {
                CodeFilePath = dataSet.CodeFilePath,
                Identity = identity,
                TypeName = dataSet.TypeName,
            };

            return output;
        }
    }
}
