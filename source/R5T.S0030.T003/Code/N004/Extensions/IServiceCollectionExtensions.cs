using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;


namespace R5T.S0030.T003.N004
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ClassContextProvider"/> implementation of <see cref="IClassContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddClassContextProvider(this IServiceCollection services,
            IServiceAction<N003.IClassContextProvider> classContextProviderAction,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction)
        {
            services
                .Run(classContextProviderAction)
                .Run(compilationUnitContextProviderAction)
                .Run(namespaceContextProviderAction)
                .AddSingleton<IClassContextProvider, ClassContextProvider>();

            return services;
        }
    }
}
