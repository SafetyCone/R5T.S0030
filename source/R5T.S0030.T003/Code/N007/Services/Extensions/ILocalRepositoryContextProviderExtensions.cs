using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N007;

using N005 = R5T.S0030.T003.N005;
using N006 = R5T.S0030.T003.N006;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class ILocalRepositoryContextProviderExtensions
    {
        public static LocalRepositoryContext GetContext(this ILocalRepositoryContextProvider _,
            N006.ILocalRepositoryContext localRepositoryContext,
            N005.IRemoteRepositoryContext remoteRepositoryContext)
        {
            var output = new LocalRepositoryContext
            {
                LocalRepositoryContext_N006 = localRepositoryContext,
                RemoteRepositoryContext_N005 = remoteRepositoryContext,
            };

            return output;
        }

        public static async Task<string> GetRepositoryDirectoryPath(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName)
        {
            var repositoriesDirectoryPath = await localRepositoryContextProvider.RepositoriesDirectoryPathProvider.GetRepositoriesDirectoryPath();

            var repositoryDirectoryName = Instances.RepositoryNameOperator.GetRepositoryDirectoryName(repositoryName);

            var repositoryDirectoryPath = Instances.PathOperator.GetDirectoryPath(
                repositoriesDirectoryPath,
                repositoryDirectoryName);

            return repositoryDirectoryPath;
        }

        public static Task For(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            N006.ILocalRepositoryContext localRepositoryContext,
            N005.IRemoteRepositoryContext remoteRepositoryContext,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            // Get context.
            var context = localRepositoryContextProvider.GetContext(
                localRepositoryContext,
                remoteRepositoryContext);

            // Run modifier.
            return context.Modify(localRepositoryModifierAction);
        }

        public static Task For(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName,
            string directoryPath,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            return localRepositoryContextProvider.RemoteRepositoryContextProvider_N005.For(
                repositoryName,
                remoteRepositoryContext =>
                {
                    return localRepositoryContextProvider.LocalRepositoryContextProvider_N006.For(
                        directoryPath,
                        localRepositoryContext =>
                        {
                            return localRepositoryContextProvider.For(
                                localRepositoryContext,
                                remoteRepositoryContext,
                                localRepositoryModifierAction);
                        });
                });
        }

        public static async Task For(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            // Get the directory path.
            var repositoryDirectoryPath = await localRepositoryContextProvider.GetRepositoryDirectoryPath(repositoryName);

            await localRepositoryContextProvider.For(
                repositoryName,
                repositoryDirectoryPath,
                localRepositoryModifierAction);
        }

        public static Task In(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName,
            string directoryPath,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            return localRepositoryContextProvider.RemoteRepositoryContextProvider_N005.In(
                repositoryName,
                remoteRepositoryContext =>
                {
                    return localRepositoryContextProvider.LocalRepositoryContextProvider_N006.In(
                        directoryPath,
                        localRepositoryContext =>
                        {
                            return localRepositoryContextProvider.For(
                                localRepositoryContext,
                                remoteRepositoryContext,
                                localRepositoryModifierAction);
                        });
                });
        }

        public static async Task In(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            var directoryPath = await localRepositoryContextProvider.GetRepositoryDirectoryPath(repositoryName);

            await localRepositoryContextProvider.In(
                repositoryName,
                directoryPath,
                localRepositoryModifierAction);
        }

        // No acquired. Repositories are too important to acquire (create if they don't exist, or use if they do).
        // Callers should know whether the repository exists.

        /// <summary>
        /// Creates the remote repository, then clones the repository locally.
        /// </summary>
        public static Task InCreatedThenCloned(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName,
            string description,
            bool isPrivate,
            string directoryPath,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            return localRepositoryContextProvider.RemoteRepositoryContextProvider_N005.InCreated(
                repositoryName,
                description,
                isPrivate,
                async remoteRepositoryContext =>
                {
                    var cloneUrl = await remoteRepositoryContext.RemoteRepositoryContext_N005.GetCloneUrl();

                    await localRepositoryContextProvider.LocalRepositoryContextProvider_N006.InCloned(
                        directoryPath,
                        cloneUrl,
                        localRepositoryContext =>
                        {
                            return localRepositoryContextProvider.For(
                                localRepositoryContext,
                                remoteRepositoryContext,
                                localRepositoryModifierAction);
                        });
                });
        }

        public static async Task InCreatedThenCloned(this ILocalRepositoryContextProvider localRepositoryContextProvider,
            string repositoryName,
            string description,
            bool isPrivate,
            Func<ILocalRepositoryContext, Task> localRepositoryModifierAction)
        {
            var directoryPath = await localRepositoryContextProvider.GetRepositoryDirectoryPath(repositoryName);

            await localRepositoryContextProvider.InCreatedThenCloned(
                repositoryName,
                description,
                isPrivate,
                directoryPath,
                localRepositoryModifierAction);
        }
    }
}
