using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.F002;
using R5T.S0030.T003.N012;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    using InterfaceAnnotation = ISyntaxNodeAnnotation<InterfaceDeclarationSyntax>;
    using NamespaceAnnotation = ISyntaxNodeAnnotation<NamespaceDeclarationSyntax>;

    using InterfaceModifierAction = Func<CompilationUnitSyntax, IInterfaceContext, Task<CompilationUnitSyntax>>;


    public static partial class IInterfaceContextProviderExtensions
    {
        public static InterfaceContext GetContext(this IInterfaceContextProvider _,
            InterfaceAnnotation annotation)
        {
            var context = new InterfaceContext
            {
                Annotation = annotation,
            };

            return context;
        }

        public static async Task<CompilationUnitSyntax> For(this IInterfaceContextProvider interfaceContextProvider,
            InterfaceAnnotation interfaceAnnotation,
            Func<IInterfaceContext, Task<CompilationUnitSyntax>> interfaceCompilationUnitGenerator)
        {
            // Get the context.
            var context = interfaceContextProvider.GetContext(interfaceAnnotation);

            // Modify the context.
            var output = await context.Modify(interfaceCompilationUnitGenerator);
            return output;
        }

        public static async Task<CompilationUnitSyntax> For(this IInterfaceContextProvider interfaceContextProvider,
            CompilationUnitSyntax compilationUnit,
            InterfaceAnnotation interfaceAnnotation,
            InterfaceModifierAction interfaceModifierAction)
        {
            // Get the context.
            var context = interfaceContextProvider.GetContext(interfaceAnnotation);

            // Modify the context.
            compilationUnit = await context.Modify(
                compilationUnit,
                interfaceModifierAction);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> In(this IInterfaceContextProvider interfaceContextProvider,
            string interfaceName,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            InterfaceModifierAction interfaceModifierAction)
        {
            var hasInterface = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.HasInterface(interfaceName));

            if (!hasInterface)
            {
                throw new Exception($"Interface '{interfaceName}' not found in namespace '{namespaceAnnotation.GetFullName(compilationUnit)}'.");
            }

            // ?? Not sure why this here, does it work? 20220502
            var @namespace = hasInterface.Result.Annotate_Typed(out var interfaceAnnotation);

            compilationUnit = await interfaceContextProvider.For(
                compilationUnit,
                interfaceAnnotation,
                interfaceModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> In(this IInterfaceContextProvider interfaceContextProvider,
            CompilationUnitSyntax compilationUnit,
            InterfaceAnnotation interfaceAnnotation,
            InterfaceModifierAction interfaceModifierAction)
        {
            return interfaceContextProvider.For(
                compilationUnit,
                interfaceAnnotation,
                interfaceModifierAction);
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this IInterfaceContextProvider interfaceContextProvider,
            string interfaceName,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<string, Task<InterfaceDeclarationSyntax>> interfaceConstructor,
            Func<NamespaceDeclarationSyntax, InterfaceDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addInterfaceAction,
            InterfaceModifierAction interfaceModifierAction)
        {
            var hasInterface = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.HasInterface(interfaceName));

            var @interface = hasInterface
                ? hasInterface.Result
                : await interfaceConstructor(interfaceName)
                ;

            // Annotate the interface before it is added.
            @interface = @interface.Annotate_Typed(out var interfaceAnnotation);

            if (!hasInterface)
            {
                compilationUnit = await namespaceAnnotation.Modify(
                    compilationUnit,
                    async @namespace => await addInterfaceAction(
                        @namespace,
                        @interface));
            }

            compilationUnit = await interfaceContextProvider.For(
                compilationUnit,
                interfaceAnnotation,
                interfaceModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> InAcquired(this IInterfaceContextProvider interfaceContextProvider,
            string interfaceName,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            InterfaceModifierAction interfaceModifierAction)
        {
            return interfaceContextProvider.InAcquired(
                interfaceName,
                namespaceAnnotation,
                compilationUnit,
                Instances.InterfaceGenerator.GetPublicInterface,
                Instances.InterfaceOperator.AddInterface,
                interfaceModifierAction);
        }

        public static async Task<CompilationUnitSyntax> InCreated(this IInterfaceContextProvider interfaceContextProvider,
            string interfaceName,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<string, Task<InterfaceDeclarationSyntax>> interfaceConstructor,
            Func<NamespaceDeclarationSyntax, InterfaceDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addInterfaceAction,
            InterfaceModifierAction interfaceModifierAction)
        {
            var @interface = await interfaceConstructor(interfaceName);

            // Annotate the interface before adding to the namespace.
            @interface = @interface.Annotate_Typed(out var interfaceAnnotation);

            compilationUnit = await namespaceAnnotation.Modify(
                compilationUnit,
                async @namespace => await addInterfaceAction(
                    @namespace,
                    @interface));

            compilationUnit = await interfaceContextProvider.For(
                compilationUnit,
                interfaceAnnotation,
                interfaceModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> InCreated(this IInterfaceContextProvider interfaceContextProvider,
            string interfaceName,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            InterfaceModifierAction interfaceModifierAction,
            Func<NamespaceDeclarationSyntax, InterfaceDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addInterfaceAction)
        {
            return interfaceContextProvider.InCreated(
                interfaceName,
                namespaceAnnotation,
                compilationUnit,
                Instances.InterfaceGenerator.GetInterface,
                addInterfaceAction,
                interfaceModifierAction);
        }

        public static Task<CompilationUnitSyntax> InCreated(this IInterfaceContextProvider interfaceContextProvider,
            string interfaceName,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            InterfaceModifierAction interfaceModifierAction)
        {
            return interfaceContextProvider.InCreated(
                interfaceName,
                namespaceAnnotation,
                compilationUnit,
                interfaceModifierAction,
                Instances.InterfaceOperator.AddInterface);
        }
    }
}
