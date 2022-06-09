using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.S0030.T003.N009;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IHasProjectContextExtensions
    {
        public static void DeleteProjectDirectory_OkIfNotExist(this IHasProjectContext hasContext)
        {
            var projectDirectoryPath = hasContext.ProjectDirectoryPath();

            Instances.FileSystemOperator.DeleteDirectoryOkIfNotExists(projectDirectoryPath);
        }

        public static void DeleteProjectFile_OkIfNotExists(this IHasProjectContext hasContext)
        {
            var context = hasContext.ProjectContext_N009;

            Instances.FileSystemOperator.DeleteFileOkIfNotExists(context.ProjectFilePath);
        }

        public static string ProjectDirectoryPath(this IHasProjectContext hasContext)
        {
            var context = hasContext.ProjectContext_N009;

            var output = Instances.PathOperator.GetDirectoryPathOfFilePath(context.ProjectFilePath);
            return output;
        }

        public static string[] GetSolutionFilesInDirectoryOrDirectParentDirectories(this IHasProjectContext hasContext)
        {
            var context = hasContext.ProjectContext_N009;

            var output = Instances.FileSystemOperator.FindSolutionFilesInFileDirectoryOrDirectParentDirectories(
                context.ProjectFilePath)
                .Now();

            return output;
        }

        public static string GetProjectDirectoryRelativeFilePath(this IHasProjectContext hasContext,
            string projectDirectoryRelativeFilePath)
        {
            var context = hasContext.ProjectContext_N009;

            var projectDirectoryPath = context.ProjectDirectoryPath();

            var output = Instances.PathOperator.Combine(
                projectDirectoryPath,
                projectDirectoryRelativeFilePath);

            return output;
        }

        public static string GetProjectDirectoryRelativeFilePath(this IHasProjectContext hasContext,
            Func<string, string> projectDirectoryRelativeFilePathProvider)
        {
            var context = hasContext.ProjectContext_N009;

            var projectDirectoryPath = context.ProjectDirectoryPath();

            var output = projectDirectoryRelativeFilePathProvider(projectDirectoryPath);
            return output;
        }

        public static async Task Modify(this IHasProjectContext hasContext,
            Func<IProjectContext, Task> projectModifierAction)
        {
            await projectModifierAction(hasContext.ProjectContext_N009);
        }

        public static string GetProjectName(this IHasProjectContext hasContext)
        {
            var context = hasContext.ProjectContext_N009;

            var output = Instances.ProjectPathsOperator.GetProjectName(
                context.ProjectFilePath);

            return output;
        }

        public static bool ProjectDirectoryExists(this IHasProjectContext hasContext)
        {
            var projectDirectoryPath = hasContext.ProjectDirectoryPath();

            var output = Instances.FileSystemOperator.DirectoryExists(projectDirectoryPath);
            return output;
        }

        public static bool ProjectFileExists(this IHasProjectContext hasContext)
        {
            var context = hasContext.ProjectContext_N009;

            var output = Instances.FileSystemOperator.FileExists(context.ProjectFilePath);
            return output;
        }
    }
}
