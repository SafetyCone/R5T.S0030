using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.S0030.T003.N008;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IHasSolutionContextExtensions
    {
        public static async Task AddDependencyProjectReferences(this IHasSolutionContext hasContext,
            IEnumerable<string> projectFilePaths)
        {
            var context = hasContext.SolutionContext;

            await context.VisualStudioSolutionFileOperator.AddDependencyProjectReferences(
                context.SolutionFilePath,
                projectFilePaths);
        }

        public static async Task AddDependencyProjectReference(this IHasSolutionContext hasContext,
            string projectFilePath)
        {
            var context = hasContext.SolutionContext;

            await context.VisualStudioSolutionFileOperator.AddDependencyProjectReference(
                context.SolutionFilePath,
                projectFilePath);
        }

        public static async Task AddDependencyProjectReferences_Recursive(this IHasSolutionContext hasContext,
            IEnumerable<string> projectFilePaths)
        {
            var context = hasContext.SolutionContext;

            await Instances.SolutionOperator.AddDependencyProjectReferencesAndRecursiveDependencies(
                context.SolutionFilePath,
                projectFilePaths,
                context.StringlyTypedPathOperator,
                context.VisualStudioProjectFileReferencesProvider,
                context.VisualStudioSolutionFileOperator);
        }

        public static Task AddDependencyProjectReference_Recursive(this IHasSolutionContext hasContext,
            string projectFilePath)
        {
            return hasContext.AddDependencyProjectReferences_Recursive(
                EnumerableHelper.From(projectFilePath));
        }

        public static async Task AddProjectReferences(this IHasSolutionContext hasContext,
            IEnumerable<string> projectFilePaths)
        {
            var context = hasContext.SolutionContext;

            await context.VisualStudioSolutionFileOperator.AddProjectReferences(
                context.SolutionFilePath,
                projectFilePaths);
        }

        public static async Task AddProjectReference(this IHasSolutionContext hasContext,
            string projectFilePath)
        {
            var context = hasContext.SolutionContext;

            await context.VisualStudioSolutionFileOperator.AddProjectReference(
                context.SolutionFilePath,
                projectFilePath);
        }

        public static async Task<bool> HasProjectReference(this IHasSolutionContext hasContext,
            string projectReferenceFilePath)
        {
            var context = hasContext.SolutionContext;

            var output = await context.VisualStudioSolutionFileOperator.HasProjectReference(
                context.SolutionFilePath,
                projectReferenceFilePath,
                context.StringlyTypedPathOperator);

            return output;
        }

        public static async Task<Dictionary<string, bool>> HasProjectReferences(this IHasSolutionContext hasContext,
            IEnumerable<string> projectReferenceFilePaths)
        {
            var context = hasContext.SolutionContext;

            var output = await context.VisualStudioSolutionFileOperator.HasProjectReferences(
                context.SolutionFilePath,
                projectReferenceFilePaths,
                context.StringlyTypedPathOperator);

            return output;
        }

        public static Task<string[]> ListProjectReferences(this IHasSolutionContext hasContext)
        {
            var context = hasContext.SolutionContext;

            return context.VisualStudioSolutionFileOperator.ListProjectReferencePaths(
                context.SolutionFilePath,
                context.StringlyTypedPathOperator);
        }

        public static Task Modify(this IHasSolutionContext hasContext,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            return solutionModifierAction(hasContext.SolutionContext);
        }

        public static async Task RemoveProjectReference(this IHasSolutionContext hasContext,
            string projectFilePath)
        {
            var context = hasContext.SolutionContext;

            await context.VisualStudioSolutionFileOperator.RemoveProjectReference(
                context.SolutionFilePath,
                projectFilePath);
        }

        public static string SolutionDirectoryPath(this IHasSolutionContext hasContext)
        {
            var context = hasContext.SolutionContext;

            var output = Instances.PathOperator.GetDirectoryPathOfFilePath(context.SolutionFilePath);
            return output;
        }

        public static bool SolutionDirectoryExists(this IHasSolutionContext hasContext)
        {
            var solutionDirectoryPath = hasContext.SolutionDirectoryPath();

            var output = Instances.FileSystemOperator.DirectoryExists(solutionDirectoryPath);
            return output;
        }

        public static bool SolutionFileExists(this IHasSolutionContext hasContext)
        {
            var output = Instances.FileSystemOperator.FileExists(
                hasContext.SolutionContext.SolutionFilePath);

            return output;
        }
    }
}
