using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N010;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class ICodeFileContextProviderExtensions
    {
        public static CodeFileContext GetContext(this ICodeFileContextProvider _,
            string codeFilePath)
        {
            var output = new CodeFileContext
            {
                CodeFilePath = codeFilePath,
            };

            return output;
        }

        public static Task For(this ICodeFileContextProvider codeFileContextProvider,
            string codeFilePath,
            Func<ICodeFileContext, Task> codeFileModifierAction)
        {
            // Get the context.
            var context = codeFileContextProvider.GetContext(
                codeFilePath);

            // Modify.
            return context.Modify(codeFileModifierAction);
        }

        // No In(), InCreated(), or InAcquired() for code file path, since it's really only the compilation unit that matters (nothing to create for the code file).

        //public static Task In(this ICodeFileContextProvider codeFileContextProvider,
        //    string codeFilePath,
        //    Func<ICodeFileContext, Task> codeFileModifierAction)
        //{
        //    // In() errors if the solution file does not exist.
        //    codeFileContextProvider.VerifyCodeFileExists(codeFilePath);

        //    return codeFileContextProvider.For(
        //        codeFilePath,
        //        codeFileModifierAction);
        //}

        //public static Task InCreated(this ICodeFileContextProvider codeFileContextProvider,
        //    string codeFilePath,
        //    Func<ICodeFileContext, Task> codeFileModifierAction)
        //{
        //    // In() errors if the solution file does not exist.
        //    codeFileContextProvider.VerifyCodeFileExists(codeFilePath);

        //    return codeFileContextProvider.For(
        //        codeFilePath,
        //        codeFileModifierAction);
        //}

        public static bool CodeFileExists(this ICodeFileContextProvider _,
            string projectFilePath)
        {
            var output = Instances.FileSystemOperator.DirectoryExists(projectFilePath);
            return output;
        }

        public static void VerifyCodeFileDoesNotExist(this ICodeFileContextProvider _,
            string projectFilePath)
        {
            var exists = _.CodeFileExists(projectFilePath);
            if (exists)
            {
                throw new Exception($"Code file already exists:\n{projectFilePath}");
            }
        }

        public static void VerifyCodeFileExists(this ICodeFileContextProvider _,
            string projectFilePath)
        {
            var exists = _.CodeFileExists(projectFilePath);
            if (!exists)
            {
                throw new Exception($"Code file does not exist:\n{projectFilePath}");
            }
        }
    }
}
