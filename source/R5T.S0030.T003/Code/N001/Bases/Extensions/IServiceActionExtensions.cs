using System;

using R5T.D0116;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N001
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="CompilationUnitContextProvider"/> implementation of <see cref="ICompilationUnitContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ICompilationUnitContextProvider> AddCompilationUnitContextProviderAction(this IServiceAction _,
            IServiceAction<IUsingDirectivesFormatter> usingDirectivesFormatterAction)
        {
            var serviceAction = _.New<ICompilationUnitContextProvider>(services => services.AddCompilationUnitContextProvider(
                usingDirectivesFormatterAction));

            return serviceAction;
        }
    }
}
