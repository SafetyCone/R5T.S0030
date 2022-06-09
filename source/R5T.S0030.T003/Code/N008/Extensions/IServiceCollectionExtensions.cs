using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0078;
using R5T.D0083;
using R5T.T0063;


namespace R5T.S0030.T003.N008
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="SolutionContextProvider"/> implementation of <see cref="ISolutionContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddSolutionContextProvider(this IServiceCollection services,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            services
                .Run(stringlyTypedPathOperatorAction)
                .Run(visualStudioProjectFileReferencesProviderAction)
                .Run(visualStudioSolutionFileOperatorAction)
                .AddSingleton<ISolutionContextProvider, SolutionContextProvider>();

            return services;
        }
    }
}
