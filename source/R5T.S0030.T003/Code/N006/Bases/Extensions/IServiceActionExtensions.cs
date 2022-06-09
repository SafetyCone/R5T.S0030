using System;

using R5T.D0037;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N006
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="LocalRepositoryContextProvider"/> implementation of <see cref="ILocalRepositoryContextProvider"/> as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ILocalRepositoryContextProvider> AddLocalRepositoryContextProviderAction(this IServiceAction _,
            IServiceAction<IGitOperator> gitOperatorAction)
        {
            var serviceAction = _.New<ILocalRepositoryContextProvider>(services => services.AddLocalRepositoryContextProvider(
                gitOperatorAction));

            return serviceAction;
        }
    }
}
