using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N008;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class ISolutionContextProviderExtensions
    {
        public static ISolutionContext GetContext(this ISolutionContextProvider solutionContextProvider,
            string solutionFilePath)
        {
            var output = new SolutionContext
            {
                StringlyTypedPathOperator = solutionContextProvider.StringlyTypedPathOperator,
                VisualStudioProjectFileReferencesProvider = solutionContextProvider.VisualStudioProjectFileReferencesProvider,
                VisualStudioSolutionFileOperator = solutionContextProvider.VisualStudioSolutionFileOperator,

                SolutionFilePath = solutionFilePath,
            };

            return output;
        }

        public static async Task For(this ISolutionContextProvider solutionContextProvider,
            Func<Task<string>> solutionFilePathProvider,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            var solutionFilePath = await solutionFilePathProvider();

            await solutionContextProvider.For(
                solutionFilePath,
                solutionModifierAction);
        }

        public static async Task For(this ISolutionContextProvider solutionContextProvider,
            string solutionFilePath,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            // Get context.
            var context = solutionContextProvider.GetContext(
                solutionFilePath);

            // Modify context.
            await context.Modify(solutionModifierAction);
        }

        public static Task In(this ISolutionContextProvider solutionContextProvider,
            string solutionFilePath,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            // In() errors if the solution file does not exist.
            solutionContextProvider.VerifySolutionFileExists(solutionFilePath);

            return solutionContextProvider.For(
                solutionFilePath,
                solutionModifierAction);
        }

        public static async Task In(this ISolutionContextProvider solutionContextProvider,
            Func<Task<string>> solutionFilePathProvider,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            var solutionFilePath = await solutionFilePathProvider();

            await solutionContextProvider.In(
                solutionFilePath,
                solutionModifierAction);
        }

        public static Task InAcquired(this ISolutionContextProvider solutionContextProvider,
            string solutionFilePath,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            var exists = solutionContextProvider.SolutionFileExists(solutionFilePath);

            Func<string, Func<ISolutionContext, Task>, Task> function = exists
                ? solutionContextProvider.In
                : solutionContextProvider.InCreated
                ;

            return function(
                solutionFilePath,
                solutionModifierAction);
        }

        public static async Task InAcquired(this ISolutionContextProvider solutionContextProvider,
            Func<Task<string>> solutionFilePathProvider,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            var solutionFilePath = await solutionFilePathProvider();

            await solutionContextProvider.InAcquired(
                solutionFilePath,
                solutionModifierAction);
        }

        public static async Task InCreated(this ISolutionContextProvider solutionContextProvider,
            string solutionFilePath,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            // InCreated() errors if the solution file exists.
            solutionContextProvider.VerifySolutionFileDoesNotExist(
                solutionFilePath);

            // Create the solution file.
            await solutionContextProvider.VisualStudioSolutionFileOperator.Create(
                solutionFilePath);

            // Modify.
            await solutionContextProvider.For(
                solutionFilePath,
                solutionModifierAction);
        }

        public static async Task InCreated(this ISolutionContextProvider solutionContextProvider,
            Func<Task<string>> solutionFilePathProvider,
            Func<ISolutionContext, Task> solutionModifierAction)
        {
            var solutionFilePath = await solutionFilePathProvider();

            await solutionContextProvider.InCreated(
                solutionFilePath,
                solutionModifierAction);
        }

        public static bool SolutionFileExists(this ISolutionContextProvider _,
            string solutionFilePath)
        {
            var output = Instances.FileSystemOperator.FileExists(solutionFilePath);
            return output;
        }

        public static void VerifySolutionFileDoesNotExist(this ISolutionContextProvider _,
            string solutionFilePath)
        {
            var exists = _.SolutionFileExists(solutionFilePath);
            if (exists)
            {
                throw new Exception($"Solution file already exists:\n{solutionFilePath}");
            }
        }

        public static void VerifySolutionFileExists(this ISolutionContextProvider _,
            string solutionFilePath)
        {
            var exists = _.SolutionFileExists(solutionFilePath);
            if (!exists)
            {
                throw new Exception($"Solution file does not exist:\n{solutionFilePath}");
            }
        }
    }
}
