using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0098;


namespace R5T.S0030
{
    public static class IOperationExtensions
    {
        public static async Task<ITypeNamedCodeFilePathed[]> GetTypeNamedCodeFilePatheds(this IOperation _,
            string codeFilepath,
            Func<CompilationUnitSyntax, Task<string[]>> getTypeNamesFromCompilationUnitFunction)
        {
            // Be defensive.
            var fileExists = Instances.FileSystemOperator.FileExists(codeFilepath);
            if (!fileExists)
            {
                return Array.Empty<ITypeNamedCodeFilePathed>();
            }

            var compilationUnit = await Instances.CompilationUnitOperator.Load(
                codeFilepath);

            var typeNames = await getTypeNamesFromCompilationUnitFunction(compilationUnit);

            var output = typeNames
                .Select(typeName => new TypeNamedCodeFilePathed
                {
                    CodeFilePath = codeFilepath,
                    TypeName = typeName,
                })
                .Now();

            return output;
        }

        public static async Task<ServiceComponentDescriptor[]> CheckDirectoryForInterfaces(this IOperation _,
            string directoryPath,
            string projectFilePath)
        {
            // Be defensive.
            var directoryExists = Instances.FileSystemOperator.DirectoryExists(directoryPath);
            if(!directoryExists)
            {
                return Array.Empty<ServiceComponentDescriptor>();
            }

            var possibleServiceComponentDescriptors = new List<ServiceComponentDescriptor>();

            var codeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(directoryPath);

            foreach (var codeFilePath in codeFilePaths)
            {
                var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                    codeFilePath,
                    compilationUnit =>
                    {
                        var interfaces = compilationUnit.GetInterfaces();

                        var interfaceTypeNames = interfaces.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                        return Task.FromResult(interfaceTypeNames);
                    });

                var currentServiceComponentDescriptors = typeNamedCodeFilePaths
                    .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                    .Now();

                possibleServiceComponentDescriptors.AddRange(currentServiceComponentDescriptors);
            }

            return possibleServiceComponentDescriptors.ToArray();
        }

        public static async Task<ServiceComponentDescriptor[]> CheckDirectoryForClasses(this IOperation _,
            string directoryPath,
            string projectFilePath)
        {
            // Be defensive.
            var directoryExists = Instances.FileSystemOperator.DirectoryExists(directoryPath);
            if (!directoryExists)
            {
                return Array.Empty<ServiceComponentDescriptor>();
            }

            var possibleServiceComponentDescriptors = new List<ServiceComponentDescriptor>();

            var codeFilePaths = Instances.FileSystemOperator.EnumerateAllDescendentFilePaths(directoryPath);

            foreach (var codeFilePath in codeFilePaths)
            {
                var typeNamedCodeFilePaths = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                    codeFilePath,
                    compilationUnit =>
                    {
                        var classes = compilationUnit.GetClasses();

                        var classTypeNames = classes.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                        return Task.FromResult(classTypeNames);
                    });

                var currentServiceComponentDescriptors = typeNamedCodeFilePaths
                    .Select(x => x.GetServiceComponentDescriptor(projectFilePath))
                    .Now();

                possibleServiceComponentDescriptors.AddRange(currentServiceComponentDescriptors);
            }

            return possibleServiceComponentDescriptors.ToArray();
        }
    }
}
