using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N006;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IHasLocalRepositoryContextExtensions
    {
        public static async Task Clone(this IHasLocalRepositoryContext hasContext,
            string cloneUrl)
        {
            var context = hasContext.LocalRepositoryContext_N006;

            await context.GitOperator.Clone(
                cloneUrl,
                context.DirectoryPath);
        }

        public static void DeleteLocalRepositoryDirectory(this IHasLocalRepositoryContext hasContext)
        {
            Instances.FileSystemOperator.DeleteDirectory(hasContext.LocalRepositoryContext_N006.DirectoryPath);
        }

        public static string GetSourceSolutionFilePath(this IHasLocalRepositoryContext hasContext,
            string solutionName)
        {
            var context = hasContext.LocalRepositoryContext_N006;

            var repositoryDirectoryPath = context.RepositoryDirectoryPath();

            var solutionFilePath = Instances.SolutionPathsOperator.GetSourceSolutionFilePath(
                repositoryDirectoryPath,
                solutionName);

            return solutionFilePath;
        }

        public static Task Modify(this IHasLocalRepositoryContext hasContext,
            Func<ILocalRepositoryContext, Task> localRepositoryContextAction)
        {
            return localRepositoryContextAction(hasContext.LocalRepositoryContext_N006);
        }

        public static string RepositoryDirectoryPath(this IHasLocalRepositoryContext hasContext)
        {
            var output = hasContext.LocalRepositoryContext_N006.DirectoryPath;
            return output;
        }
    }
}
