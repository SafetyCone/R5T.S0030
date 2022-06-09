using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N005;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class IRemoteRepositoryContextProviderExtensions
    {
        public static RemoteRepositoryContext GetContext(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            string repositoryName)
        {
            var output = new RemoteRepositoryContext
            {
                GitHubOperator = remoteRepositoryContextProvider.GitHubOperator,
                Name = repositoryName,
            };

            return output;
        }

        public static async Task For(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            string repositoryName,
            Func<IRemoteRepositoryContext, Task> remoteRepositoryModifierAction)
        {
            // Create context.
            var context = remoteRepositoryContextProvider.GetContext(
                repositoryName);

            // Run modifier.
            await context.Modify(remoteRepositoryModifierAction);
        }

        public static async Task In(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            string repositoryName,
            Func<IRemoteRepositoryContext, Task> remoteRepositoryModifierAction)
        {
            // If not exists, In() errors.
            await remoteRepositoryContextProvider.VerifyRemoteRepositoryExists(repositoryName);

            await remoteRepositoryContextProvider.For(
                repositoryName,
                remoteRepositoryModifierAction);
        }

        ///// <summary>
        ///// Although InAcquired() is less useful here (caller has to specify visibility and description for a repository that may already exist)
        ///// Note: values are not checked, and then we would have to check those values, or communicate that they are ignored.
        ///// </summary>
        ///// <param name="remoteRepositoryContextProvider"></param>
        ///// <param name="repositoryName"></param>
        ///// <param name="description"></param>
        ///// <param name="isPrivate"></param>
        ///// <param name="remoteRepositoryModifierAction"></param>
        ///// <returns></returns>
        //public static async Task InAcquired(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
        //    string repositoryName,
        //    string description,
        //    bool isPrivate,
        //    Func<IRemoteRepositoryContext, Task> remoteRepositoryModifierAction)
        //{

        //}

        // No InAcquired(), since the caller would have to specify visibility and description for a repository that may already exist, values are not checked, and then we would have to check those values, or communicate that they are ignored.
        // Best to just make the caller know whether the repository has been created, or not, or use For().

        public static async Task InCreated(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            string repositoryName,
            string description,
            bool isPrivate,
            Func<IRemoteRepositoryContext, Task> remoteRepositoryModifierAction)
        {
            // If exists already, InCreated() errors.
            await remoteRepositoryContextProvider.VerifyRemoteRepositoryDoesNotExist(repositoryName);

            // Create repository.
            var repositorySpecification = Instances.GitHubRepositorySpecificationGenerator.GetSafetyConeDefault(
                repositoryName,
                description,
                isPrivate);

            // Ignore repository identifier returned.
            await remoteRepositoryContextProvider.GitHubOperator.CreateRepositoryNonIdempotent(repositorySpecification);

            await remoteRepositoryContextProvider.For(
                repositoryName,
                remoteRepositoryModifierAction);
        }

        public static async Task VerifyRemoteRepositoryDoesNotExist(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            string repositoryName)
        {
            var exists = await remoteRepositoryContextProvider.GitHubOperator.RepositoryExists_SafetyCone(repositoryName);
            if (exists)
            {
                throw new Exception($"{repositoryName}: Remote repository already exists.");
            }
        }

        public static async Task VerifyRemoteRepositoryExists(this IRemoteRepositoryContextProvider remoteRepositoryContextProvider,
            string repositoryName)
        {
            var exists = await remoteRepositoryContextProvider.GitHubOperator.RepositoryExists_SafetyCone(repositoryName);
            if (!exists)
            {
                throw new Exception($"{repositoryName}: Remote repository does not exist.");
            }
        }
    }
}
