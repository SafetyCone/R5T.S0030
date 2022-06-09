using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;


namespace R5T.S0030.T003.N010
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="CodeFileContextProvider"/> implementation of <see cref="ICodeFileContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddCodeFileContextProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICodeFileContextProvider, CodeFileContextProvider>();

            return services;
        }
    }
}
