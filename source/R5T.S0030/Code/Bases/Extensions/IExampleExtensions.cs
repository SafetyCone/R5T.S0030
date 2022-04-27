using System;

using R5T.B0005;


namespace R5T.S0030
{
    public static class IExampleExtensions
    {
        public static string CodeFilePath(this IExample _)
        {
            var output = @"C:\Temp\Code.cs";
            return output;
        }

        public static ServiceImplementationDescriptor BasicServiceImplementationDescriptor(this IExample _)
        {
            ServiceImplementationDescriptor output = new()
            {
                ProjectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj",
                CodeFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\Code\Services\Implementations\ProjectFilePathsProvider.cs",

                NamespacedTypeName = "R5T.S0030.ProjectFilePathsProvider",

                HasServiceDefinition = true,
                HasServiceDependencies = true,

                ServiceDefinitionNamespacedTypeName = "R5T.S0030.IProjectFilePathsProvider",
                ServiceDependencyNamespacedTypeNames = new[]
                {
                    "R5T.D0101.IProjectRepository",
                },
            };

            return output;
        }

        public static string LocalNamespaceName_R5T_S0029(this IExample _)
        {
            return "R5T.S0029";
        }

        public static string NamespaceName(this IExample _)
        {
            return "Example.Namespace";
        }
    }
}
