using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N006;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class ILocalRepositoryContextProviderExtensions
    {
        public static LocalRepositoryContext GetContext(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string directoryPath)
        {
            var output = new LocalRepositoryContext
            {
                DirectoryPath = directoryPath,
                GitOperator = localRepositoryContextProvider.GitOperator,
            };

            return output;
        }

        public static async Task For(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string directoryPath,
            Func<ILocalRepositoryContext, Task> localRepositoryContextModifier)
        {
            // Get the context.
            var context = localRepositoryContextProvider.GetContext(
                directoryPath);

            // Modify the context.
            await context.Modify(localRepositoryContextModifier);
        }

        public static async Task In(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string directoryPath,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            // If not exists, In() errors.
            localRepositoryContextProvider.VerifyLocalRepositoryExists(directoryPath);

            await localRepositoryContextProvider.For(
                directoryPath,
                localRepositoryModifierAction);
        }

        // No InAcquired(), since that would require callers to know the clone URL even for repositories that exist. Save InAcquired() for the level-02 local repository context provider.

        /// <summary>
        /// Similar to InCreated(), except for local repositories that are cloned from remote repositories.
        /// </summary>
        public static async Task InCloned(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string directoryPath,
            string cloneUrl,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            // If already exists, InCreated() errors.
            localRepositoryContextProvider.VerifyLocalRepositoryDoesNotExist(directoryPath);

            // Clone to the directory.
            await localRepositoryContextProvider.GitOperator.Clone(
                cloneUrl,
                directoryPath);

            // Now modify.
            await localRepositoryContextProvider.For(
                directoryPath,
                localRepositoryModifierAction);
        }

        public static void VerifyLocalRepositoryDoesNotExist(this ILocalRepositoryContextProvider _,
            string directoryPath)
        {
            var exists = Instances.FileSystemOperator.DirectoryExists(directoryPath);
            if (exists)
            {
                throw new Exception($"Local repository already exists:\n{directoryPath}");
            }
        }

        public static void VerifyLocalRepositoryExists(this ILocalRepositoryContextProvider _,
            string directoryPath)
        {
            var exists = Instances.FileSystemOperator.DirectoryExists(directoryPath);
            if (!exists)
            {
                throw new Exception($"Local repository does not exist:\n{directoryPath}");
            }
        }
    }
}
