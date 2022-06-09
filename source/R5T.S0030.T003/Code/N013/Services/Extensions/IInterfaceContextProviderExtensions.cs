using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Extensions;

using R5T.T0126;

using R5T.S0030.F002;
using R5T.S0030.T003.N013;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N012 = R5T.S0030.T003.N012;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    using InterfaceCreationResult = SyntaxNodeCreationResult<CompilationUnitSyntax, InterfaceDeclarationSyntax>;


    public static partial class IInterfaceContextProviderExtensions
    {
        public static InterfaceContext GetContext(this IInterfaceContextProvider _,
             N001.ICompilationUnitContext compilationUnitContext,
             N012.IInterfaceContext interfaceContext,
             N002.INamespaceContext namespaceContext)
        {
            var context = new InterfaceContext
            {
                CompilationUnitContext_N001 = compilationUnitContext,
                InterfaceContext_N012 = interfaceContext,
                NamespaceContext_N002 = namespaceContext,
            };

            return context;
        }

        public static async Task<InterfaceCreationResult> InCreated(this IInterfaceContextProvider interfaceContextProvider,
            string namespacedTypeNameForInterface,
            Func<string, Task<InterfaceDeclarationSyntax>> interfaceConstructor,
            Func<NamespaceDeclarationSyntax, InterfaceDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addInterfaceAction,
            Func<CompilationUnitSyntax, IInterfaceContext, Task<CompilationUnitSyntax>> interfaceModificationAction,
            Action<N001.CompilationUnitContextOptions> compilationContextOptionsModifier = default)
        {
            var namespaceNameForInterface = Instances.NamespacedTypeNameOperator.GetNamespaceName(
                namespacedTypeNameForInterface);

            var typeNameForInterface = Instances.NamespacedTypeNameOperator.GetTypeName(
                namespacedTypeNameForInterface);

            var interfaceAnnotation = SyntaxNodeAnnotation.Initialize<InterfaceDeclarationSyntax>();

            var compilationUnit = await interfaceContextProvider.CompilationUnitContextProvider.InCreated(
                async (compilationUnit, compilationContext) =>
                {
                    compilationUnit = await interfaceContextProvider.NamespaceContextProvider.InCreated(
                        namespaceNameForInterface,
                        compilationUnit,
                        async (compilationUnit, namespaceContext) =>
                        {
                            compilationUnit = await interfaceContextProvider.InterfaceContextProvider_N012.InCreated(
                                typeNameForInterface,
                                namespaceContext.NamespaceAnnotation,
                                compilationUnit,
                                interfaceConstructor,
                                addInterfaceAction,
                                async (compilationUnit, interfaceContext_N012) =>
                                {
                                    // Get context.
                                    var context = interfaceContextProvider.GetContext(
                                        compilationContext,
                                        interfaceContext_N012,
                                        namespaceContext);

                                    compilationUnit = await interfaceModificationAction(
                                        compilationUnit,
                                        context);

                                    interfaceAnnotation = context.Annotation;

                                    return compilationUnit;
                                });

                            return compilationUnit;
                        });

                    return compilationUnit;
                },
                compilationContextOptionsModifier);

            return SyntaxCreationResult.Node(
                compilationUnit,
                interfaceAnnotation);
        }

        public static Task<InterfaceCreationResult> InCreated(this IInterfaceContextProvider interfaceContextProvider,
            string namespacedTypeNameForInterface,
            Func<CompilationUnitSyntax, IInterfaceContext, Task<CompilationUnitSyntax>> interfaceModificationAction,
            Func<string, Task<InterfaceDeclarationSyntax>> interfaceConstructor = default,
            Func<NamespaceDeclarationSyntax, InterfaceDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addInterfaceAction = default,
            Action<N001.CompilationUnitContextOptions> compilationContextOptionsModifier = default)
        {
            interfaceConstructor = interfaceConstructor.IfIsDefaultThen(
                Instances.InterfaceGenerator.GetInterface);

            addInterfaceAction = addInterfaceAction.IfIsDefaultThen(
                Instances.InterfaceOperator.AddInterface);

            return interfaceContextProvider.InCreated(
                namespacedTypeNameForInterface,
                interfaceConstructor,
                addInterfaceAction,
                interfaceModificationAction,
                compilationContextOptionsModifier);
        }

        public static async Task<InterfaceCreationResult> In(this IInterfaceContextProvider interfaceContextProvider,
            CompilationUnitSyntax compilationUnit,
            ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> interfaceAnnotation,
            Func<CompilationUnitSyntax, IInterfaceContext, Task<CompilationUnitSyntax>> interfaceModificationAction,
            Action<N001.CompilationUnitContextOptions> optionsModifierAction = default)
        {
            compilationUnit = await interfaceContextProvider.CompilationUnitContextProvider.In(
                compilationUnit,
                async (compilationUnit, compilationContext) =>
                {
                    compilationUnit = interfaceAnnotation.GetContainingNamespaceAnnotation(
                        compilationUnit,
                        out var namespaceAnnotation);

                    compilationUnit = await interfaceContextProvider.NamespaceContextProvider.In(
                        compilationUnit,
                        namespaceAnnotation,
                        async (compilationUnit, namespaceContext) =>
                        {
                            compilationUnit = await interfaceContextProvider.InterfaceContextProvider_N012.In(
                                compilationUnit,
                                interfaceAnnotation,
                                async (compilationUnit, interfaceContext_N012) =>
                                {
                                    // Get context.
                                    var context = interfaceContextProvider.GetContext(
                                        compilationContext,
                                        interfaceContext_N012,
                                        namespaceContext);

                                    compilationUnit = await interfaceModificationAction(
                                        compilationUnit,
                                        context);

                                    return compilationUnit;
                                });

                            return compilationUnit;
                        });

                    return compilationUnit;
                },
                optionsModifierAction);

            return SyntaxCreationResult.Node(
                compilationUnit,
                interfaceAnnotation);
        }
    }
}
