using System;

using Microsoft.Extensions.DependencyInjection;
using R5T.T0062;

using R5T.D0105;
using R5T.T0063;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;
using N004 = R5T.S0030.T003.N004;
using N005 = R5T.S0030.T003.N005;
using N007 = R5T.S0030.T003.N007;
using N008 = R5T.S0030.T003.N008;
using N009 = R5T.S0030.T003.N009;
using N010 = R5T.S0030.T003.N010;
using N013 = R5T.S0030.T003.N013;


namespace R5T.S0030
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="E001_CodeElementCreation"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<E001_CodeElementCreation> AddE001_CodeElementCreationAction(this IServiceAction _,
            IServiceAction<N003.IClassContextProvider> classContextProviderAction_N003,
            IServiceAction<N004.IClassContextProvider> classContextProviderAction_N004,
            IServiceAction<N010.ICodeFileContextProvider> codeFileContextProviderAction_N010,
            IServiceAction<N001.ICompilationUnitContextProvider> compilationUnitContextProviderAction,
            IServiceAction<N013.IInterfaceContextProvider> interfaceContextProviderAction,
            IServiceAction<N007.ILocalRepositoryContextProvider> localRepositoryContextProviderAction,
            IServiceAction<N002.INamespaceContextProvider> namespaceContextProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<N009.IProjectContextProvider> projectContextProviderAction,
            IServiceAction<N005.IRemoteRepositoryContextProvider> remoteRepositoryContextProviderAction,
            IServiceAction<IServiceImplementationCodeFilePathsProvider> serviceImplementationCodeFilePathsProviderAction,
            IServiceAction<Repositories.IServiceRepository> serviceRepositoryAction,
            IServiceAction<N008.ISolutionContextProvider> solutionContextProviderAction)
        {
            var serviceAction = _.New<E001_CodeElementCreation>(services => services.AddE001_CodeElementCreation(
                classContextProviderAction_N003,
                classContextProviderAction_N004,
                codeFileContextProviderAction_N010,
                compilationUnitContextProviderAction,
                interfaceContextProviderAction,
                localRepositoryContextProviderAction,
                namespaceContextProviderAction,
                notepadPlusPlusOperatorAction,
                projectContextProviderAction,
                remoteRepositoryContextProviderAction,
                serviceImplementationCodeFilePathsProviderAction,
                serviceRepositoryAction,
                solutionContextProviderAction));

            return serviceAction;
        }
    }
}