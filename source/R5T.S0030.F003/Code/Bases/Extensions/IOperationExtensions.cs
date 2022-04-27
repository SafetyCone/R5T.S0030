using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.F0001.F002;
using R5T.T0098;
using R5T.T0126;


namespace R5T.S0030.F003
{
    public static class IOperationExtensions
    {
        /// <summary>
        /// Searches a projects for extension methods to a specified type.
        /// </summary>
        public static async Task<ProjectResult[]> SearchForExtensions(this IOperation _,
            IEnumerable<string> projectFilePaths,
            Func<string, Task<string[]>> projectCodeFilePathsProvider,
            string extensionTypeNamespacedTypeName)
        {
            var projectResults = new List<ProjectResult>();

            foreach (var projectFilePath in projectFilePaths)
            {
                var projectResult = await _.SearchForExtensions(
                    projectFilePath,
                    projectCodeFilePathsProvider,
                    extensionTypeNamespacedTypeName);
            }

            var output = projectResults.ToArray();
            return output;
        }

        /// <summary>
        /// Searches a project for extension methods to a specified type.
        /// </summary>
        public static async Task<ProjectResult> SearchForExtensions(this IOperation _,
            string projectFilePath,
            Func<string, Task<string[]>> projectCodeFilePathsProvider,
            string extensionTypeNamespacedTypeName)
        {
            var projectCodeFilePaths = await projectCodeFilePathsProvider(projectFilePath);

            var codeFileResults = new List<CodeFileResult>();

            foreach (var codeFilePath in projectCodeFilePaths)
            {
                var codeFileResult = await _.SearchForExtensions(
                    codeFilePath,
                    extensionTypeNamespacedTypeName);

                codeFileResults.Add(codeFileResult);
            }

            var output = new ProjectResult
            {
                CodeFileResults = codeFileResults.ToArray(),
                ProjectFilePath = projectFilePath,
            };

            return output;
        }

        /// <summary>
        /// Searches a code file for extension methods to a specified type.
        /// </summary>
        public static async Task<CodeFileResult> SearchForExtensions(this IOperation _,
            string codeFilePath,
            string extensionTypeNamespacedTypeName)
        {
            var compilationUnit = await CompilationUnitSyntaxHelper.LoadFile(codeFilePath);

            var classes = compilationUnit.GetClasses();

            compilationUnit = compilationUnit.AnnotateNodes_Typed(
                classes,
                out var annotationsByClass);

            var classResults = new List<ClassResult>();

            foreach (var classAnnotation in annotationsByClass.Values)
            {
                var compilationUnitClassResult = _.SearchForExtensions(
                    compilationUnit,
                    classAnnotation,
                    extensionTypeNamespacedTypeName);

                compilationUnit = compilationUnitClassResult.CompilationUnit;

                classResults.Add(compilationUnitClassResult.Result);
            }

            var output = new CodeFileResult
            {
                ClassResults = classResults.ToArray(),
                CodeFilePath = codeFilePath,
                CompilationUnit = compilationUnit,
            };

            return output;
        }

        public static CompilationUnitResult<ClassResult> SearchForExtensions(this IOperation _,
            CompilationUnitSyntax compilationUnit,
            ISyntaxNodeAnnotation<ClassDeclarationSyntax> classAnnotation,
            string extensionTypeNamespacedTypeName)
        {
            var extensionMethods = classAnnotation.Get(
                compilationUnit,
                @class => @class.GetExtensionMethods());

            var availableNamespacedTypeNames = EnumerableHelper.From(extensionTypeNamespacedTypeName);

            var extensionMethodsOnType = extensionMethods
                .Where(xMethod =>
                {
                    var extensionParameter = xMethod.GetExtensionParameter();

                    var extensionTypeWasFound = _.TryGuessNamespacedTypeName(
                        availableNamespacedTypeNames,
                        compilationUnit,
                        extensionParameter.Type);

                    return extensionTypeWasFound.Exists;
                })
                .Now();

            compilationUnit = compilationUnit.AnnotateNodes_Typed(
                extensionMethodsOnType,
                out var annotationsByExtensionMethodOnType);

            var methodResults = annotationsByExtensionMethodOnType.Values
                .Select(xAnnotation =>
                {
                    var methodName = xAnnotation.Get(
                        compilationUnit,
                        method => method.GetNamespacedTypeNamedParameterTypedMethodName());

                    var methodResult = new MethodResult
                    {
                        Annotation = xAnnotation,
                        MethodName = methodName,
                    };

                    return methodResult;
                })
                .Now();

            var className = classAnnotation.Get(
                compilationUnit,
                @class => @class.GetNamespacedTypeName_HandlingTypeParameters());

            var classResult = new ClassResult
            {
                Annotation = classAnnotation,
                ClassName = className,
                MethodResults = methodResults,
            };

            return CompilationUnitResult.From(
                compilationUnit,
                classResult);
        }
    }
}