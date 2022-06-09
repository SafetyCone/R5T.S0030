using System;
using System.Threading.Tasks;

using R5T.D0079;

using R5T.S0030.T003.N009;

using N008 = R5T.S0030.T003.N008;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IProjectContextProviderExtensions
    {
        public static Task For(this IProjectContextProvider projectContextProvider,
            string projectName,
            N008.ISolutionContext solutionContext,
            Func<IProjectContext, Task> projectModifierAction)
        {
            var projectFilePath = projectContextProvider.GetProjectFilePath(
                projectName,
                solutionContext);

            return projectContextProvider.For(
                projectFilePath,
                projectModifierAction);
        }

        public static string GetProjectFilePath(this IProjectContextProvider _,
            string projectName,
            N008.ISolutionContext solutionContext)
        {
            var solutionDirectoryPath = solutionContext.SolutionDirectoryPath();

            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(
                solutionDirectoryPath,
                projectName);

            var projectFilePath = Instances.ProjectPathsOperator.GetProjectFilePath(
                projectDirectoryPath,
                projectName);

            return projectFilePath;
        }

        public static Task In(this IProjectContextProvider projectContextProvider,
            string projectName,
            N008.ISolutionContext solutionContext,
            Func<IProjectContext, Task> projectModifierAction)
        {
            var projectFilePath = projectContextProvider.GetProjectFilePath(
                projectName,
                solutionContext);

            return projectContextProvider.In(
                projectFilePath,
                projectModifierAction);
        }

        public static Task InAcquired(this IProjectContextProvider projectContextProvider,
            string projectName,
            N008.ISolutionContext solutionContext,
            VisualStudioProjectType projectType,
            Func<IProjectContext, Task> projectModifierAction)
        {
            var projectFilePath = projectContextProvider.GetProjectFilePath(
                projectName,
                solutionContext);

            return projectContextProvider.InAcquired(
                projectFilePath,
                projectType,
                projectModifierAction);
        }

        public static Task InCreated(this IProjectContextProvider projectContextProvider,
            string projectName,
            N008.ISolutionContext solutionContext,
            VisualStudioProjectType projectType,
            Func<IProjectContext, Task> projectModifierAction)
        {
            var projectFilePath = projectContextProvider.GetProjectFilePath(
                projectName,
                solutionContext);

            return projectContextProvider.InAcquired(
                projectFilePath,
                projectType,
                projectModifierAction);
        }
    }
}
