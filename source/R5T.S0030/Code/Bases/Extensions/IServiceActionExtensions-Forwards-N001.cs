using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0037;
using R5T.D0078;
using R5T.D0079;
using R5T.D0082;
using R5T.D0083;
using R5T.D0084.D002;
using R5T.D0116;
using R5T.T0063;

using R5T.T0062;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;
using N004 = R5T.S0030.T003.N004;
using N005 = R5T.S0030.T003.N005;
using N006 = R5T.S0030.T003.N006;
using N007 = R5T.S0030.T003.N007;
using N008 = R5T.S0030.T003.N008;
using N009 = R5T.S0030.T003.N009;
using N010 = R5T.S0030.T003.N010;
using N012 = R5T.S0030.T003.N012;
using N013 = R5T.S0030.T003.N013;


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

        /// <summary>
        /// Adds the <see cref="N005.RemoteRepositoryContextProvider"/> implementation of <see cref="N005.IRemoteRepositoryContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N005.IRemoteRepositoryContextProvider> AddRemoteRepositoryContextProviderAction_N005(this IServiceAction _,
            IServiceAction<IGitHubOperator> gitHubOperatorAction)
        {
            var serviceAction = N005.IServiceActionExtensions.AddRemoteRepositoryContextProviderAction(_,
                gitHubOperatorAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N006.LocalRepositoryContextProvider"/> implementation of <see cref="N006.ILocalRepositoryContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N006.ILocalRepositoryContextProvider> AddLocalRepositoryContextProviderAction_N006(this IServiceAction _,
            IServiceAction<IGitOperator> gitOperatorAction)
        {
            var serviceAction = N006.IServiceActionExtensions.AddLocalRepositoryContextProviderAction(_,
                gitOperatorAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N007.LocalRepositoryContextProvider"/> implementation of <see cref="N007.ILocalRepositoryContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N007.ILocalRepositoryContextProvider> AddLocalRepositoryContextProviderAction_N007(this IServiceAction _,
            IServiceAction<N006.ILocalRepositoryContextProvider> localRepositoryContextProviderAction,
            IServiceAction<N005.IRemoteRepositoryContextProvider> remoteRepositoryContextProviderAction,
            IServiceAction<IRepositoriesDirectoryPathProvider> repositoriesDirectoryPathProviderAction)
        {
            var serviceAction = N007.IServiceActionExtensions.AddLocalRepositoryContextProviderAction(_,
                localRepositoryContextProviderAction,
                remoteRepositoryContextProviderAction,
                repositoriesDirectoryPathProviderAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N008.SolutionContextProvider"/> implementation of <see cref="N008.ISolutionContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N008.ISolutionContextProvider> AddSolutionContextProviderAction_N008(this IServiceAction _,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            var serviceAction = N008.IServiceActionExtensions.AddSolutionContextProviderAction(_,
                stringlyTypedPathOperatorAction,
                visualStudioProjectFileReferencesProviderAction,
                visualStudioSolutionFileOperatorAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N009.ProjectContextProvider"/> implementation of <see cref="N009.IProjectContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N009.IProjectContextProvider> AddProjectContextProviderAction_N009(this IServiceAction _,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction)
        {
            var serviceAction = N009.IServiceActionExtensions.AddProjectContextProviderAction(_,
                stringlyTypedPathOperatorAction,
                visualStudioProjectFileOperatorAction);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N010.CodeFileContextProvider"/> implementation of <see cref="N010.ICodeFileContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N010.ICodeFileContextProvider> AddCodeFileContextProviderAction_N010(this IServiceAction _)
        {
            var serviceAction = N010.IServiceActionExtensions.AddCodeFileContextProviderAction(_);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N012.InterfaceContextProvider"/> implementation of <see cref="N012.IInterfaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N012.IInterfaceContextProvider> AddInterfaceContextProviderAction_N012(this IServiceAction _)
        {
            var serviceAction = N012.IServiceActionExtensions.AddInterfaceContextProviderAction(_);

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="N013.InterfaceContextProvider"/> implementation of <see cref="N013.IInterfaceContextProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<N013.IInterfaceContextProvider> AddInterfaceContextProviderAction_N013(this IServiceAction _,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N012.IInterfaceContextProvider> interfaceContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction)
        {
            var serviceAction = N013.IServiceActionExtensions.AddInterfaceContextProviderAction(_,
                compilationUnitContextProviderAction,
                interfaceContextProviderAction,
                namespaceContextProviderAction);

            return serviceAction;
        }
    }
}
