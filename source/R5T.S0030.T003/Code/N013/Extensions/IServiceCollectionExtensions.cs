using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;


namespace R5T.S0030.T003.N013
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="InterfaceContextProvider"/> implementation of <see cref="IInterfaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddInterfaceContextProvider(this IServiceCollection services,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N012.IInterfaceContextProvider> interfaceContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction)
        {
            services
                .Run(compilationUnitContextProviderAction)
                .Run(interfaceContextProviderAction)
                .Run(namespaceContextProviderAction)
                .AddSingleton<IInterfaceContextProvider, InterfaceContextProvider>();

            return services;
        }
    }
}
