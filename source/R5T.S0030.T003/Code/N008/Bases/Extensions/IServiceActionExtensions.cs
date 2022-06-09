using System;

using R5T.Lombardy;

using R5T.D0078;
using R5T.D0083;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N008
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="SolutionContextProvider"/> implementation of <see cref="ISolutionContextProvider"/> as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ISolutionContextProvider> AddSolutionContextProviderAction(this IServiceAction _,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            var serviceAction = _.New<ISolutionContextProvider>(services => services.AddSolutionContextProvider(
                stringlyTypedPathOperatorAction,
                visualStudioProjectFileReferencesProviderAction,
                visualStudioSolutionFileOperatorAction));

            return serviceAction;
        }
    }
}
