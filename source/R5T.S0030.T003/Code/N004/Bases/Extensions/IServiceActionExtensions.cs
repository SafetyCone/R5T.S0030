using System;

using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N004
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ClassContextProvider"/> implementation of <see cref="IClassContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IClassContextProvider> AddClassContextProviderAction(this IServiceAction _,
            IServiceAction<N003.IClassContextProvider> classContextProviderAction,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction)
        {
            var serviceAction = _.New<IClassContextProvider>(services => services.AddClassContextProvider(
                classContextProviderAction,
                compilationUnitContextProviderAction,
                namespaceContextProviderAction));

            return serviceAction;
        }
    }
}
