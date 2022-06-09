using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N007;


namespace System
{
    public static partial class IHasLocalRepositoryContextExtensions
    {
        public static async Task Clone(this IHasLocalRepositoryContext hasContext)
        {
            var context = hasContext.LocalRepositoryContext_N007;

            var cloneUrl = await context.GetCloneUrl();

            await context.Clone(cloneUrl);
        }

        public static Task Modify(this IHasLocalRepositoryContext hasContext,
            Func<ILocalRepositoryContext, Task> localRepositoryContextAction)
        {
            return localRepositoryContextAction(hasContext.LocalRepositoryContext_N007);
        }
    }
}
