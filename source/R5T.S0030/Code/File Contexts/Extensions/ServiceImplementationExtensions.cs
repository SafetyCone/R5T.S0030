using System;

using AppType = R5T.S0030.Repositories.ServiceDefinition;
using EntityType = R5T.S0030.FileContexts.Entities.ServiceDefinition;


namespace System
{
    public static partial class ServiceDefinitionExtensions
    {
        public static AppType ToAppType(this EntityType entityType)
        {
            var output = new AppType
            {
                Identity = entityType.Identity,
                CodeFilePath = entityType.CodeFilePath,
                TypeName = entityType.TypeName,
            };

            return output;
        }

        public static EntityType ToEntityType(this AppType appType)
        {
            var output = new EntityType
            {
                CodeFilePath = appType.CodeFilePath,
                Identity = appType.Identity,
                TypeName = appType.TypeName,
            };

            return output;
        }
    }
}
