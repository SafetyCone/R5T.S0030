using System;

using R5T.D0116;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N002
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="NamespaceContextProvider"/> implementation of <see cref="INamespaceContextProvider"/> as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<INamespaceContextProvider> AddNamespaceContextProviderAction(this IServiceAction _,
            IServiceAction<IUsingDirectivesFormatter> usingDirectivesFormatterAction)
        {
            var serviceAction = _.New<INamespaceContextProvider>(services => services.AddNamespaceContextProvider(
                usingDirectivesFormatterAction));

            return serviceAction;
        }
    }
}
