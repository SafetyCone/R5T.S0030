using System;

using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N010
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="CodeFileContextProvider"/> implementation of <see cref="ICodeFileContextProvider"/> as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ICodeFileContextProvider> AddCodeFileContextProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<ICodeFileContextProvider>(services => services.AddCodeFileContextProvider());
            return serviceAction;
        }
    }
}
