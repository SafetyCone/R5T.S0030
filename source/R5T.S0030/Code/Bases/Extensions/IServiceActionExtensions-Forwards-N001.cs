using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0116;
using R5T.T0063;

using R5T.T0062;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;
using N004 = R5T.S0030.T003.N004;


namespace R5T.S0030
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="N001.CompilationUnitContextProvider"/> implementation of <see cref="N001.ICompilationUnitContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N001.ICompilationUnitContextProvider> AddCompilationUnitContextProviderAction_N001(this IServiceAction _,
            IServiceAction<IUsingDirectivesFormatter> usingDirectivesFormatterAction)
        {
            var serviceAction = N001.IServiceActionExtensions.AddCompilationUnitContextProviderAction(_,
                usingDirectivesFormatterAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N002.NamespaceContextProvider"/> implementation of <see cref="N002.INamespaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N002.INamespaceContextProvider> AddNamespaceContextProviderAction_N002(this IServiceAction _,
            IServiceAction<IUsingDirectivesFormatter> usingDirectivesFormatterAction)
        {
            var serviceAction = N002.IServiceActionExtensions.AddNamespaceContextProviderAction(_,
                usingDirectivesFormatterAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N003.ClassContextProvider"/> implementation of <see cref="N003.IClassContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N003.IClassContextProvider> AddClassContextProviderAction_N003(this IServiceAction _)
        {
            var serviceAction = N003.IServiceActionExtensions.AddClassContextProviderAction(_);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N004.ClassContextProvider"/> implementation of <see cref="N004.IClassContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N004.IClassContextProvider> AddClassContextProviderAction_N004(this IServiceAction _,
            IServiceAction<N003.IClassContextProvider> classContextProviderAction,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction)
        {
            var serviceAction = N004.IServiceActionExtensions.AddClassContextProviderAction(_,
                classContextProviderAction,
                compilationUnitContextProviderAction,
                namespaceContextProviderAction);

            return serviceAction;
        }
    }
}