using System;

using R5T.T0062;
using R5T.T0063;


namespace R5T.S0030.T003.N003
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ClassContextProvider"/> implementation of <see cref="IClassContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IClassContextProvider> AddClassContextProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IClassContextProvider>(services => services.AddClassContextProvider());
            return serviceAction;
        }
    }
}
