using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Extensions;

using R5T.S0030.T003.N004;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IClassContextProviderExtensions
    {
        public static Task<ClassContext> GetContext(this IClassContextProvider _,
             N003.IClassContext classContext,
             N001.ICompilationUnitContext compilationUnitContext,
             N002.INamespaceContext namespaceContext)
        {
            var context = new ClassContext
            {
                ClassContext_N003 = classContext,
                CompilationUnitContext_N001 = compilationUnitContext,
                NamespaceContext_N002 = namespaceContext,
            };

            return Task.FromResult(context);
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string codeFilePath,
            string namespacedTypeNameForClass,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> classModificationAction,
            Action<N001.CompilationUnitContextOptions> compilationContextOptionsModifier = default)
        {
            var namespaceNameForClass = Instances.NamespacedTypeNameOperator.GetNamespaceName(
                namespacedTypeNameForClass);

            var typeNameForClass = Instances.NamespacedTypeNameOperator.GetTypeName(
                namespacedTypeNameForClass);

            var compilationUnit = await classContextProvider.CompilationUnitContextProvider.InAcquired(
                codeFilePath,
                async (compilationUnit, compilationContext) =>
                {
                    compilationUnit = await classContextProvider.NamespaceContextProvider.InAcquired(
                        namespaceNameForClass,
                        compilationUnit,
                        async (compilationUnit, namespaceContext) =>
                        {
                            compilationUnit = await classContextProvider.ClassContextProvider_N003.InAcquired(
                                typeNameForClass,
                                namespaceContext.Annotation,
                                compilationUnit,
                                classConstructor,
                                addClassAction,
                                async (compilationUnit, classContext) =>
                                {
                                    // Get context.
                                    var context = await classContextProvider.GetContext(
                                        classContext,
                                        compilationContext,
                                        namespaceContext);

                                    compilationUnit = await classModificationAction(
                                        compilationUnit,
                                        context);

                                    return compilationUnit;
                                });

                            return compilationUnit;
                        });

                    return compilationUnit;
                },
                compilationContextOptionsModifier);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string codeFilePath,
            string namespacedTypeNameForClass,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> classModificationAction,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor = default,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction = default,
            Action<N001.CompilationUnitContextOptions> compilationContextOptionsModifier = default)
        {
            classConstructor = classConstructor.IfIsDefaultThen(
                Instances.ClassGenerator.GetClass_Latest);

            addClassAction = addClassAction.IfIsDefaultThen(
                Instances.ClassOperator.AddClass_Latest);

            return classContextProvider.InAcquired(
                codeFilePath,
                namespacedTypeNameForClass,
                classConstructor,
                addClassAction,
                classModificationAction,
                compilationContextOptionsModifier);
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string namespacedTypeNameForClass,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> classModificationAction,
            Action<N001.CompilationUnitContextOptions> compilationContextOptionsModifier = default)
        {
            var namespaceNameForClass = Instances.NamespacedTypeNameOperator.GetNamespaceName(
                namespacedTypeNameForClass);

            var typeNameForClass = Instances.NamespacedTypeNameOperator.GetTypeName(
                namespacedTypeNameForClass);

            var compilationUnit = await classContextProvider.CompilationUnitContextProvider.InCreated(
                async (compilationUnit, compilationContext) =>
                {
                    compilationUnit = await classContextProvider.NamespaceContextProvider.InAcquired(
                        namespaceNameForClass,
                        compilationUnit,
                        async (compilationUnit, namespaceContext) =>
                        {
                            compilationUnit = await classContextProvider.ClassContextProvider_N003.InAcquired(
                                typeNameForClass,
                                namespaceContext.Annotation,
                                compilationUnit,
                                classConstructor,
                                addClassAction,
                                async (compilationUnit, classContext) =>
                                {
                                    // Get context.
                                    var context = await classContextProvider.GetContext(
                                        classContext,
                                        compilationContext,
                                        namespaceContext);

                                    compilationUnit = await classModificationAction(
                                        compilationUnit,
                                        context);

                                    return compilationUnit;
                                });

                            return compilationUnit;
                        });

                    return compilationUnit;
                },
                compilationContextOptionsModifier);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string namespacedTypeNameForClass,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> classModificationAction,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor = default,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction = default,
            Action<N001.CompilationUnitContextOptions> compilationContextOptionsModifier = default)
        {
            classConstructor = classConstructor.IfIsDefaultThen(
                Instances.ClassGenerator.GetClass_Latest);

            addClassAction = addClassAction.IfIsDefaultThen(
                Instances.ClassOperator.AddClass_Latest);

            return classContextProvider.InAcquired(
                namespacedTypeNameForClass,
                classConstructor,
                addClassAction,
                classModificationAction,
                compilationContextOptionsModifier);
        }
    }
}
