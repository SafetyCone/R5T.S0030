using System;

using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N013
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="InterfaceContextProvider"/> implementation of <see cref="IInterfaceContextProvider"/> as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IInterfaceContextProvider> AddInterfaceContextProviderAction(this IServiceAction _,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N012.IInterfaceContextProvider> interfaceContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction)
        {
            var serviceAction = _.New<IInterfaceContextProvider>(services => services.AddInterfaceContextProvider(
                compilationUnitContextProviderAction,
                interfaceContextProviderAction,
                namespaceContextProviderAction));

            return serviceAction;
        }
    }
}
