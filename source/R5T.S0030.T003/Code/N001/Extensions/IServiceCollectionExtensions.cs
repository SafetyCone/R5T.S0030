using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0116;
using R5T.T0063;


namespace R5T.S0030.T003.N001
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="CompilationUnitContextProvider"/> implementation of <see cref="ICompilationUnitContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddCompilationUnitContextProvider(this IServiceCollection services,
            IServiceAction<IUsingDirectivesFormatter> usingDirectivesFormatterAction)
        {
            services
                .Run(usingDirectivesFormatterAction)
                .AddSingleton<ICompilationUnitContextProvider, CompilationUnitContextProvider>();

            return services;
        }
    }
}
