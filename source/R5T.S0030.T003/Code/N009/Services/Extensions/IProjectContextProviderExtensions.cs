using System;
using System.Threading.Tasks;

using R5T.D0079;

using R5T.S0030.T003.N009;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IProjectContextProviderExtensions
    {
        public static ProjectContext GetContext(this IProjectContextProvider projectContextProvider,
            string projectFilePath)
        {
            var output = new ProjectContext
            {
                StringlyTypedPathOperator = projectContextProvider.StringlyTypedPathOperator,
                VisualStudioProjectFileOperator = projectContextProvider.VisualStudioProjectFileOperator,

                ProjectFilePath = projectFilePath,
            };

            return output;
        }

        public static async Task For(this IProjectContextProvider projectContextProvider,
            string projectFilePath,
            Func<IProjectContext, Task> projectModifierAction)
        {
            // Get the context.
            var context = projectContextProvider.GetContext(
                projectFilePath);

            // Modify.
            await context.Modify(projectModifierAction);
        }

        public static Task In(this IProjectContextProvider projectContextProvider,
            string projectFilePath,
            Func<IProjectContext, Task> projectModifierAction)
        {
            // In() errors if the solution file does not exist.
            projectContextProvider.VerifyProjectFileExists(projectFilePath);

            return projectContextProvider.For(
                projectFilePath,
                projectModifierAction);
        }

        public static Task InAcquired(this IProjectContextProvider projectContextProvider,
            string projectFilePath,
            VisualStudioProjectType projectType,
            Func<IProjectContext, Task> projectModifierAction)
        {
            var exists = projectContextProvider.ProjectFileExists(projectFilePath);
            if(exists)
            {
                // Check project type, error if incorrect.
                //TODO

                return projectContextProvider.In(
                    projectFilePath,
                    projectModifierAction);
            }
            else
            {
                return projectContextProvider.InCreated(
                    projectFilePath,
                    projectType,
                    projectModifierAction);
            }
        }

        public static async Task InCreated(this IProjectContextProvider projectContextProvider,
            string projectFilePath,
            VisualStudioProjectType projectType,
            Func<IProjectContext, Task> projectModifierAction)
        {
            // InCreated() errors if the solution file exists.
            projectContextProvider.VerifyProjectFileDoesNotExist(projectFilePath);

            // Create the project file.
            await projectContextProvider.VisualStudioProjectFileOperator.Create(
                projectFilePath,
                projectType);

            // Modify.
            await projectContextProvider.For(
                projectFilePath,
                projectModifierAction);
        }

        public static bool ProjectFileExists(this IProjectContextProvider _,
            string projectFilePath)
        {
            var output = Instances.FileSystemOperator.FileExists(projectFilePath);
            return output;
        }

        public static void VerifyProjectFileDoesNotExist(this IProjectContextProvider _,
            string projectFilePath)
        {
            var exists = _.ProjectFileExists(projectFilePath);
            if (exists)
            {
                throw new Exception($"Project file already exists:\n{projectFilePath}");
            }
        }

        public static void VerifyProjectFileExists(this IProjectContextProvider _,
            string projectFilePath)
        {
            var exists = _.ProjectFileExists(projectFilePath);
            if (!exists)
            {
                throw new Exception($"Project file does not exist:\n{projectFilePath}");
            }
        }
    }
}
