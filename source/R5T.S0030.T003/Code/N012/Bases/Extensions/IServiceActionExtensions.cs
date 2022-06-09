using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N012
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="InterfaceContextProvider"/> implementation of <see cref="IInterfaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IInterfaceContextProvider> AddInterfaceContextProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IInterfaceContextProvider>(services => services.AddInterfaceContextProvider());
            return serviceAction;
        }
    }
}
