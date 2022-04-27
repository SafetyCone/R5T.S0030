using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0105;
using R5T.T0063;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;
using N004 = R5T.S0030.T003.N004;


namespace R5T.S0030
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="E001_CodeElementCreation"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddE001_CodeElementCreation(this IServiceCollection services,
            IServiceAction<N003.IClassContextProvider> classContextProviderAction_N003,
            IServiceAction<N004.IClassContextProvider> classContextProviderAction_N004,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction)
        {
            services
                .Run(classContextProviderAction_N003)
                .Run(classContextProviderAction_N004)
                .Run(compilationUnitContextProviderAction)
                .Run(namespaceContextProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(serviceImplementationCodeFilePathsProviderAction)
                .Run(serviceRepositoryAction)
                .AddSingleton<E001_CodeElementCreation>();

            return services;
        }
    }
}