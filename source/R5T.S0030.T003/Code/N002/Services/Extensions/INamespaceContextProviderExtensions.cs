using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003.N002;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    using NamespaceAnnotation = ISyntaxNodeAnnotation<NamespaceDeclarationSyntax>;


    public static class INamespaceContextProviderExtensions
    {
        public static NamespaceContext GetContext(this INamespaceContextProvider namespaceContextProvider,
            NamespaceAnnotation namespaceAnnotation)
        {
            var context = new NamespaceContext
            {
                NamespaceAnnotation = namespaceAnnotation,
                UsingDirectivesFormatter = namespaceContextProvider.UsingDirectivesFormatter,
            };

            return context;
        }

        public static async Task<CompilationUnitSyntax> For(this INamespaceContextProvider namespaceContextProvider,
            NamespaceAnnotation namespaceAnnotation,
            Func<INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            // Get the context.
            var context = namespaceContextProvider.GetContext(
                namespaceAnnotation);

            var compilationUnit = await context.Modify(afterAdditionNamespaceModifierAction);
            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> For(this INamespaceContextProvider namespaceContextProvider,
            CompilationUnitSyntax compilationUnit,
            NamespaceAnnotation namespaceAnnotation,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            // Get the context.
            var context = namespaceContextProvider.GetContext(
                namespaceAnnotation);

            compilationUnit = await context.Modify(
                compilationUnit,
                afterAdditionNamespaceModifierAction);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> In(this INamespaceContextProvider namespaceContextProvider,
            string namespaceName,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            var hasNamespace = compilationUnit.HasNamespace_HandleNested(namespaceName);
            if(!hasNamespace)
            {
                throw new Exception($"Namespace name '{namespaceName}' not found.");
            }

            var @namespace = hasNamespace.Result.AnnotateTyped(out var namespaceAnnotation);

            compilationUnit = await namespaceContextProvider.For(
                compilationUnit,
                namespaceAnnotation,
                afterAdditionNamespaceModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> In(this INamespaceContextProvider namespaceContextProvider,
            CompilationUnitSyntax compilationUnit,
            NamespaceAnnotation namespaceAnnotation,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            return namespaceContextProvider.For(
                compilationUnit,
                namespaceAnnotation,
                afterAdditionNamespaceModifierAction);
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this INamespaceContextProvider namespaceContextProvider,
            string namespaceName,
            CompilationUnitSyntax compilationUnit,
            Func<string, Task<NamespaceDeclarationSyntax>> namespaceConstructor,
            Func<CompilationUnitSyntax, NamespaceDeclarationSyntax, Task<CompilationUnitSyntax>> addNamespaceAction,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            var hasNamespace = compilationUnit.HasNamespace_HandleNested(namespaceName);

            var @namespace = hasNamespace
                ? hasNamespace.Result
                : await namespaceConstructor(namespaceName)
                ;

            @namespace = @namespace.AnnotateTyped(out var namespaceAnnotation);

            if(!hasNamespace)
            {
                compilationUnit = await addNamespaceAction(compilationUnit, @namespace);
            }

            compilationUnit = await namespaceContextProvider.For(
                compilationUnit,
                namespaceAnnotation,
                afterAdditionNamespaceModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> InAcquired(this INamespaceContextProvider namespaceContextProvider,
            string namespaceName,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            return namespaceContextProvider.InAcquired(
                namespaceName,
                compilationUnit,
                Instances.NamespaceGenerator.GetNewNamespace_Latest,
                Instances.NamespaceOperator.AddNamespace_WithLeadingSeparation,
                afterAdditionNamespaceModifierAction);
        }

        /// <summary>
        /// Creates a namespace, adds it to the compilation unit, and calls the modifier action.
        /// </summary>
        /// <param name="afterAdditionNamespaceModifierAction">An action run after the namespace has been added to the compilation unit.</param>
        /// <param name="addNamespaceAction">Action to add a namespace to the compilation unit.</param>
        public static async Task<CompilationUnitSyntax> InCreated(this INamespaceContextProvider namespaceContextProvider,
            string namespaceName,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction,
            Func<CompilationUnitSyntax, NamespaceDeclarationSyntax, Task<CompilationUnitSyntax>> addNamespaceAction)
        {
            // Provide a new, annotated, namespace.
            var @namespace = Instances.NamespaceGenerator.GetNewNamespace_LatestSynchronous(namespaceName)
                .AnnotateTyped(out var namespaceAnnotation);

            // Add namespace to the compilation unit.
            compilationUnit = await addNamespaceAction(
                compilationUnit,
                @namespace);

            compilationUnit = await namespaceContextProvider.For(
                compilationUnit,
                namespaceAnnotation,
                afterAdditionNamespaceModifierAction);

            return compilationUnit;
        }

        /// <inheritdoc cref="InCreated(INamespaceContextProvider, string, CompilationUnitSyntax, Func{CompilationUnitSyntax, INamespaceContext, Task{CompilationUnitSyntax}}, Func{CompilationUnitSyntax, NamespaceDeclarationSyntax, Task{CompilationUnitSyntax}})"/>
        public static Task<CompilationUnitSyntax> InCreated(this INamespaceContextProvider namespaceContextProvider,
            string namespaceName,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, INamespaceContext, Task<CompilationUnitSyntax>> afterAdditionNamespaceModifierAction)
        {
            var output = namespaceContextProvider.InCreated(
                namespaceName,
                compilationUnit,
                afterAdditionNamespaceModifierAction,
                Instances.NamespaceOperator.AddNamespace_WithLeadingSeparation);

            return output;
        }
    }
}
